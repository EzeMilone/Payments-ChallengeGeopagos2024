using Payments.DB;
using Payments.Entities;
using Payments.Entities.DTO;
using static Payments.Helpers.CustomTypes;

namespace Payments.Services
{
    /// <summary>
    /// Servicio encargado de gestionar las solicitudes de autorización de pago.
    /// </summary>
    public class AuthorizationService
    {
        private DatabaseContext context;
        private PaymentProcessorService paymentProcessorService;

        /// <summary>
        /// Constructor del servicio que inyecta la dependencia del servicio de procesamiento de pagos.
        /// </summary>
        public AuthorizationService(DatabaseContext context, PaymentProcessorService paymentProcessorService) 
        {
            this.context = context;
            this.paymentProcessorService = paymentProcessorService;
        }

        /// <summary>
        /// Inicia el proceso de autorización de un pago.
        /// </summary>
        public async Task<AuthorizationRequest> AuthorizePayment(AuthorizationRequestDTO requestDTO)
        {
            try
            {
                Client client = context.Clients.FirstOrDefault(x => x.Id == requestDTO.ClientID);
                if (client == null)
                {
                    throw new ArgumentException("Invalid ClientID");
                }

                if (requestDTO.Type != AuthorizationType.Charge && requestDTO.Type != AuthorizationType.Refund && requestDTO.Type != AuthorizationType.Reversal)
                {
                    throw new ArgumentException("Invalid Type");
                }

                AuthorizationRequest authorizationRequest = new AuthorizationRequest()
                {
                    ClientID = requestDTO.ClientID,
                    RequestDatetime = DateTime.UtcNow,
                    Amount = requestDTO.Amount,
                    Type = requestDTO.Type,
                    State = AuthorizationState.PendingAuthorization
                };

                context.AuthorizationRequests.Add(authorizationRequest);
                context.SaveChanges();

                if (paymentProcessorService.ValidatePaymentRequest(authorizationRequest))
                {
                    if (client.Type == ClientType.SimpleAuthorization)
                    {
                        authorizationRequest.State = AuthorizationState.Authorized;
                        ApprovedRequest approvedRequest = new ApprovedRequest()
                        {
                            AuthorizationID = authorizationRequest.Id,
                            ClientID = authorizationRequest.Client.Id,
                            Date = DateTime.UtcNow,
                            Amount = authorizationRequest.Amount
                        };
                        context.ApprovedRequests.AddAsync(approvedRequest);
                    }
                    else
                    {
                        var timer = new System.Timers.Timer(300000);
                        timer.Elapsed += (sender, e) =>
                        {
                            ValidateAuthorization(authorizationRequest);
                        };
                        timer.Start();
                        timer.AutoReset = false;
                        authorizationRequest.State = AuthorizationState.PendingConfirmation;
                    }
                    context.Update(authorizationRequest);
                }
                else
                {
                    authorizationRequest.State = AuthorizationState.Denied;
                    context.Update(authorizationRequest);
                    throw new Exception("Authorization denied");
                }
                return authorizationRequest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Valida una solicitud de autorización pendiente después del tiempo de espera de 5 minutos.
        /// </summary>
        private async void ValidateAuthorization(AuthorizationRequest authorizationRequest)
        {
            AuthorizationRequest authorizationToValidate = context.AuthorizationRequests.FirstOrDefault(x => x.Id == authorizationRequest.Id);
            if (authorizationToValidate.State == AuthorizationState.PendingConfirmation)
            {
                authorizationToValidate.State = AuthorizationState.Expired;
                context.Update(authorizationToValidate);
            }
        }

        /// <summary>
        /// Confirma una solicitud de autorización pendiente.
        /// </summary>
        public async Task<AuthorizationRequest> ConfirmAuthorization(ConfirmationDTO confirmationDTO)
        {
            try
            {
                AuthorizationRequest authorizationToConfirm = context.AuthorizationRequests.FirstOrDefault(x => x.Id == confirmationDTO.AuthorizationId);
                if (authorizationToConfirm == null)
                {
                    throw new ArgumentException("Invalid authorization ID");
                }
                else if (authorizationToConfirm.ClientID != confirmationDTO.ClientID)
                {
                    throw new UnauthorizedAccessException("Unauthorized");
                }
                else if (authorizationToConfirm.State != AuthorizationState.PendingConfirmation)
                {
                    throw new ArgumentException($"The authorization is not in state of pending confirmatio. Current state: {authorizationToConfirm.State}");
                }
                else
                {
                    authorizationToConfirm.State = AuthorizationState.Authorized;
                    context.Update(authorizationToConfirm);
                    ApprovedRequest approvedRequest = new ApprovedRequest()
                    {
                        AuthorizationID = authorizationToConfirm.Id,
                        ClientID = authorizationToConfirm.Client.Id,
                        Date = DateTime.UtcNow,
                        Amount = authorizationToConfirm.Amount
                    };
                    context.ApprovedRequests.AddAsync(approvedRequest);
                }
                return authorizationToConfirm;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
