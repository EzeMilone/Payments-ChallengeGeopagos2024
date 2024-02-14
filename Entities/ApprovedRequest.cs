using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Payments.Entities
{
    /// <summary>
    /// Represents a payment request that has been approved.
    /// </summary>
    public class ApprovedRequest
    {
        /// <summary>
        /// Unique identifier of the approved application.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the associated authorisation application.
        /// </summary>
        public int AuthorizationID { get; set; }

        /// <summary>
        /// Identifier of the client to whom the application was approved.
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// Date and time when the application was approved.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Amount approved in the application.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Reference to the associated application for authorisation.
        /// </summary>
        [ForeignKey("AuthorizationID")]
        public virtual AuthorizationRequest AuthorizationRequest { get; set; }
    }
}
