namespace Payments.Helpers
{
    /// <summary>
    /// Class defining enumerations used in the application.
    /// </summary>
    public class CustomTypes
    {
        /// <summary>
        /// Type of payment authorisation.
        /// </summary>
        public enum AuthorizationType
        {
            /// <summary>
            /// Charge made by a client.
            /// </summary>
            Charge = 1,
            /// <summary>
            /// Refund to a customer.
            /// </summary>
            Refund = 2,
            /// <summary>
            /// Reversal of a charge previously made.
            /// </summary>
            Reversal = 3
        }

        /// <summary>
        /// Type of customer in relation to payment authorisation.
        /// </summary>
        public enum ClientType
        {
            /// <summary>
            /// Client requiring only simple authorisation.
            /// </summary>
            SimpleAuthorization = 1,
            /// <summary>
            /// Customer requiring two-factor authorisation (payment confirmation required).
            /// </summary>
            DoubleFactorAuthorization = 2
        }

        /// <summary>
        /// Status of the payment authorisation process.
        /// </summary>
        public enum AuthorizationState
        {
            /// <summary>
            /// The authorisation is pending initial validation.
            /// </summary>
            PendingAuthorization = 1,
            /// <summary>
            /// The authorisation is pending manual confirmation.
            /// </summary>
            PendingConfirmation = 2,
            /// <summary>
            /// The authorisation has been approved.
            /// </summary>
            Authorized = 3,
            /// <summary>
            /// The authorisation has been refused.
            /// </summary>
            Denied = 4,
            /// <summary>
            /// The authorisation request has expired after 5 minutes.
            /// </summary>
            Expired = 5
        }
    }
}
