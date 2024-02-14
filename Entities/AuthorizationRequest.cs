using Payments.Entities.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Payments.Helpers.CustomTypes;

namespace Payments.Entities
{
    /// <summary>
    /// Represents a request for payment authorisation.
    /// </summary>
    public class AuthorizationRequest
    {
        public AuthorizationRequest() { }
        public AuthorizationRequest(AuthorizationRequestDTO requestDTO) 
        {

            ClientID = requestDTO.ClientID;
            RequestDatetime = DateTime.UtcNow;
            Amount = requestDTO.Amount;
            Type = requestDTO.Type;
            State = AuthorizationState.PendingAuthorization;
        }

        /// <summary>
        /// Unique identifier of the application for authorisation.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the customer making the request.
        /// </summary>
        public int ClientID { get; set; }

        /// <summary>
        /// Date and time when the request was made.
        /// </summary>
        public DateTime RequestDatetime { get; set; }

        /// <summary>
        /// Amount of the request for authorisation.
        /// </summary>
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// Type of authorisation requested (Charge, Refund, Reversal).
        /// </summary>
        public AuthorizationType Type { get; set; }

        /// <summary>
        /// Current status of the authorisation request (Pending, Authorised, Denied, etc.).
        /// </summary>
        public AuthorizationState State { get; set; }

        /// <summary>
        /// Reference to the client associated with the application.
        /// </summary>
        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }
    }
}
