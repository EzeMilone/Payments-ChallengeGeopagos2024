using static Payments.Helpers.CustomTypes;

namespace Payments.Entities.DTO
{
    /// <summary>
    /// Data transfer object representing a request for payment authorisation.
    /// </summary>
    public class AuthorizationRequestDTO
    {
        /// <summary>
        /// Amount of the request for authorisation.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Type of authorisation requested (Charge, Refund, Reversal).
        /// </summary>
        public AuthorizationType Type { get; set; }

        /// <summary>
        /// Identifier of the customer making the request.
        /// </summary>
        /// <remarks>
        /// In a real scenario, this field would be obtained from the authenticated user making the request.
        /// It is included here for practical purposes only.
        /// </remarks>
        public int ClientID { get; set; }
    }
}
