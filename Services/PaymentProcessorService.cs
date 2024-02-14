using Payments.Entities;

namespace Payments.Services
{
    /// <summary>
    /// Service responsible for validating payment authorisation requests.
    /// </summary>
    public class PaymentProcessorService
    {
        /// <summary>
        /// Validates a payment authorisation request on the basis of a simple criterion.
        /// </summary>
        /// <param name="request">The application for authorisation to be validated.</param>
        /// <returns>True if the request is valid (the amount does not contain decimals), False otherwise.</returns>
        public bool ValidatePaymentRequest(AuthorizationRequest request)
        {
            // Check that the amount has no decimals.
            return Math.Truncate(request.Amount) == request.Amount;
        }
    }
}

