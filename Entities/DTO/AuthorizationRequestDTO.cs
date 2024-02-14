using static Payments.Helpers.CustomTypes;

namespace Payments.Entities.DTO
{
    /// <summary>
    /// Objeto de transferencia de datos que representa una solicitud de autorización de pago.
    /// </summary>
    public class AuthorizationRequestDTO
    {
        /// <summary>
        /// Monto de la solicitud de autorización.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Tipo de autorización solicitada (Cargo, Reembolso, Reversal).
        /// </summary>
        public AuthorizationType Type { get; set; }

        /// <summary>
        /// Identificador del cliente que realiza la solicitud.
        /// </summary>
        /// <remarks>
        /// En un escenario real, este campo se obtendría del usuario autenticado que realiza la solicitud.
        /// Se incluye aquí solo para fines prácticos.
        /// </remarks>
        public int ClientID { get; set; }
    }
}
