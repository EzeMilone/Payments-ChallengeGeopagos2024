namespace Payments.Entities.DTO
{
    /// <summary>
    /// Objeto de transferencia de datos que representa una confirmación de una solicitud de autorización de pago.
    /// </summary>
    public class ConfirmationDTO
    {
        /// <summary>
        /// Identificador de la solicitud de autorización que se confirma.
        /// </summary>
        public int AuthorizationId { get; set; }
        public int ClientID { get; set; }
    }
}
