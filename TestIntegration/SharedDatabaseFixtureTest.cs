using Alojat.Data;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace TestIntegration
{
    public class SharedDatabaseFixtureTest 
    {

        private const string ConnectionString = "Server=DESKTOP-TM7A2FI;Database=AlquilerDB;User ID=sa;Password=sqAnder#tho; MultipleActiveResultSets=True;";
        public AlquilerDbContext CreateContext(DbTransaction? transaction = null)
        {
            var context = new AlquilerDbContext(new DbContextOptionsBuilder<AlquilerDbContext>()
                .UseSqlServer(ConnectionString)
                .Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;

        }

    }
}
