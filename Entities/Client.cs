using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Payments.Helpers.CustomTypes;

namespace Payments.Entities
{
    /// <summary>
    /// Represents a customer who can make payment authorisation requests.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Unique customer identifier.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Client's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of customer in relation to payment authorisation.
        /// </summary>
        public ClientType Type { get; set; }
    }
}
