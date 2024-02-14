namespace Payments.Helpers
{
    /// <summary>
    /// Clase que define enumeraciones utilizadas en la aplicación.
    /// </summary>
    public class CustomTypes
    {
        /// <summary>
        /// Tipo de autorización de pago.
        /// </summary>
        public enum AuthorizationType
        {
            /// <summary>
            /// Cargo realizado a un cliente.
            /// </summary>
            Charge = 1,
            /// <summary>
            /// Reembolso a un cliente.
            /// </summary>
            Refund = 2,
            /// <summary>
            /// Anulación de un cargo previamente realizado.
            /// </summary>
            Reversal = 3
        }

        /// <summary>
        /// Tipo de cliente en relación a la autorización de pago.
        /// </summary>
        public enum ClientType
        {
            /// <summary>
            /// Cliente que solo requiere autorización simple.
            /// </summary>
            SimpleAuthorization = 1,
            /// <summary>
            /// Cliente que requiere autorización de doble factor (necesario confirmacion del pago).
            /// </summary>
            DoubleFactorAuthorization = 2
        }

        /// <summary>
        /// Estado del proceso de autorización de pago.
        /// </summary>
        public enum AuthorizationState
        {
            /// <summary>
            /// La autorización está pendiente de la validación inicial.
            /// </summary>
            PendingAuthorization = 1,
            /// <summary>
            /// La autorización está pendiente de confirmación manual.
            /// </summary>
            PendingConfirmation = 2,
            /// <summary>
            /// La autorización ha sido aprobada.
            /// </summary>
            Authorized = 3,
            /// <summary>
            /// La autorización ha sido denegada.
            /// </summary>
            Denied = 4,
            /// <summary>
            /// La solicitud de autorización ha expirado pasados los 5 minutos.
            /// </summary>
            Expired = 5
        }
    }
}
