using Microsoft.EntityFrameworkCore;
using Payments.Entities;

namespace Payments.DB
{
    /// <summary>
    /// Contexto de la base de datos para la aplicación.
    /// </summary>
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<AuthorizationRequest> AuthorizationRequests { get; set; }
        public DbSet<ApprovedRequest> ApprovedRequests { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
