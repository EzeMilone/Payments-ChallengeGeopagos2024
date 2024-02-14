using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Payments.Helpers.CustomTypes;

namespace Payments.Entities
{
    /// <summary>
    /// Representa un cliente que puede realizar solicitudes de autorización de pago.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Identificador único del cliente.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Nombre del cliente.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Tipo de cliente en relación a la autorización de pago.
        /// </summary>
        public ClientType Type { get; set; }
    }
}
