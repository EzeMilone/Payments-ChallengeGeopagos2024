using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payments.Entities
{
    /// <summary>
    /// Representa una solicitud de pago que ha sido aprobada.
    /// </summary>
    public class ApprovedRequest
    {
        /// <summary>
        /// Identificador único de la solicitud aprobada.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Identificador de la solicitud de autorización asociada.
        /// </summary>
        public int AuthorizationID { get; set; }

        /// <summary>
        /// Identificador del cliente al que se le aprobó la solicitud.
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// Fecha y hora en la que se aprobó la solicitud.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Monto aprobado en la solicitud.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Referencia a la solicitud de autorización asociada.
        /// </summary>
        [ForeignKey("AuthorizationID")]
        public virtual AuthorizationRequest AuthorizationRequest { get; set; }
    }
}
