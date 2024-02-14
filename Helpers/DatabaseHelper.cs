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
        /// Fills the database with initial data, this is purely for practical purposes.
        /// </summary>
        public void SetUpDatabase()
        {
            if(context.Clients.ToList().Count == 0)
            {
                // Add initial customers that will be used to simulate the payment authorisation request.
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