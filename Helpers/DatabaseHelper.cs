using Microsoft.EntityFrameworkCore;
using Payments.DB;
using Payments.Entities;

namespace Payments.Helpers
{
    public class DatabaseHelper
    {
        private DatabaseContext context;
        public DatabaseHelper(DatabaseContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Rellena la base de datos con datos inciales, esto con fines meramente practicos
        /// </summary>
        public void SetUpDatabase()
        {
            if(context.Clients.ToList().Count == 0)
            {
                // Agregar clientes iniciales que serviran para simular la solicitud de autorizacion de pago.
                context.Clients.AddRange(
                  new Client { Name = "John Doe", Type = CustomTypes.ClientType.SimpleAuthorization },
                  new Client { Name = "David Johnson", Type = CustomTypes.ClientType.DoubleFactorAuthorization },
                  new Client { Name = "John Wick", Type = CustomTypes.ClientType.SimpleAuthorization },
                  new Client { Name = "Pete Mitchell", Type = CustomTypes.ClientType.DoubleFactorAuthorization }
                );
                context.SaveChanges();
            }
        }
    }
}