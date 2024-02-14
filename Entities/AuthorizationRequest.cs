using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Payments.Helpers.CustomTypes;

namespace Payments.Entities
{
    /// <summary>
    /// Representa una solicitud de autorización de pago.
    /// </summary>
    public class AuthorizationRequest
    {
        /// <summary>
        /// Identificador único de la solicitud de autorización.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Identificador del cliente que realiza la solicitud.
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// Fecha y hora en la que se realizó la solicitud.
        /// </summary>
        public DateTime RequestDatetime { get; set; }

        /// <summary>
        /// Monto de la solicitud de autorización.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")] // Specify decimal precision and scale
        public decimal Amount { get; set; }

        /// <summary>
        /// Tipo de autorización solicitada (Cargo, Reembolso, Reversal).
        /// </summary>
        public AuthorizationType Type { get; set; }

        /// <summary>
        /// Estado actual de la solicitud de autorización (Pendiente, Autorizado, Denegado, etc.).
        /// </summary>
        public AuthorizationState State { get; set; }

        /// <summary>
        /// Referencia al cliente asociado a la solicitud.
        /// </summary>
        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }
    }
}
