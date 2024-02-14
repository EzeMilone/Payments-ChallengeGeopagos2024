using Payments.DB;
using Payments.Entities;
using Payments.Entities.DTO;
using static Payments.Helpers.CustomTypes;

namespace Payments.Services
{
    /// <summary>
    /// Service responsible for handling requests for payment authorisations.
    /// </summary>
    public class AuthorizationService
    {
        private DatabaseContext context;
        private PaymentProcessorService paymentProcessorService;

        /// <summary>
        /// Service builder injecting the dependency on the payment processing service.
        /// </summary>
        public AuthorizationService(DatabaseContext context, PaymentProcessorService paymentProcessorService) 
        {
            this.context = context;
            this.paymentProcessorService = paymentProcessorService;
        }

        /// <summary>
        /// Initiates the process of authorising a payment.
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

                AuthorizationRequest authorizationRequest = new AuthorizationRequest(requestDTO);

                context.AuthorizationRequests.Add(authorizationRequest);
                context.SaveChanges();

                bool isValidPaymentRequest = paymentProcessorService.ValidatePaymentRequest(authorizationRequest);
                if (!isValidPaymentRequest)
                {
                    authorizationRequest.State = AuthorizationState.Denied;
                    context.Update(authorizationRequest);
                    throw new Exception("Authorization denied");
                }
                else
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
                        context.SaveChangesAsync();
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
                    context.SaveChanges();
                }
                return authorizationRequest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Validates a pending authorisation request after the 5-minute time-out.
        /// </summary>
        private async void ValidateAuthorization(AuthorizationRequest authorizationRequest)
        {
            AuthorizationRequest authorizationToValidate = context.AuthorizationRequests.FirstOrDefault(x => x.Id == authorizationRequest.Id);
            if (authorizationToValidate.State == AuthorizationState.PendingConfirmation)
            {
                authorizationToValidate.State = AuthorizationState.Expired;
                context.Update(authorizationToValidate);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Confirm a pending authorisation request.
        /// </summary>
        public async Task<AuthorizationRequest> ConfirmAuthorization(ConfirmationDTO confirmationDTO)
        {
            try
            {
                AuthorizationRequest authorizationToConfirm = context.AuthorizationRequests.FirstOrDefault(x => x.Id == confirmationDTO.AuthorizationId);
                string errors = ValidateAuthorizationToConfirm(authorizationToConfirm, confirmationDTO);
                if (errors != string.Empty)
                {
                    throw new Exception(errors);
                }
                else
                {
                    authorizationToConfirm.State = AuthorizationState.Authorized;
                    context.Update(authorizationToConfirm);
                    context.SaveChanges();

                    ApprovedRequest approvedRequest = new ApprovedRequest()
                    {
                        AuthorizationID = authorizationToConfirm.Id,
                        ClientID = authorizationToConfirm.Client.Id,
                        Date = DateTime.UtcNow,
                        Amount = authorizationToConfirm.Amount
                    };

                    context.ApprovedRequests.AddAsync(approvedRequest);
                    context.SaveChangesAsync();
                }
                return authorizationToConfirm;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string ValidateAuthorizationToConfirm(AuthorizationRequest authorizationToConfirm, ConfirmationDTO confirmationDTO)
        {
            string message = string.Empty;
            if (authorizationToConfirm == null)
            {
                message = "Invalid authorization ID";
            }
            else if (authorizationToConfirm.ClientID != confirmationDTO.ClientID)
            {
                message = "Unauthorized";
            }
            else if (authorizationToConfirm.State != AuthorizationState.PendingConfirmation)
            {
                message = $"The authorization is not in state of pending confirmatio. Current state: {authorizationToConfirm.State}";
            }

            return message;
        }
    }
}
