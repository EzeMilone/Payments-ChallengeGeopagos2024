using Payments.Entities;

namespace Payments.Services
{
    /// <summary>
    /// Servicio responsable de validar las solicitudes de autorización de pago.
    /// </summary>
    public class PaymentProcessorService
    {
        /// <summary>
        /// Valida una solicitud de autorización de pago en base a un criterio simple.
        /// </summary>
        /// <param name="request">La solicitud de autorización a validar.</param>
        /// <returns>True si la solicitud es válida (el monto no contiene decimales), False en caso contrario.</returns>
        public bool ValidatePaymentRequest(AuthorizationRequest request)
        {
            // Verifica que el monto no tenga decimales
            return Math.Truncate(request.Amount) == request.Amount;
        }
    }
}

