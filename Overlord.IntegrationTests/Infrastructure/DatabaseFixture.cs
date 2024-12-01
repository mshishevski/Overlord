using Microsoft.EntityFrameworkCore;
using Overlord.DataAccess;
using Overlord.Tests.Data;

namespace Overlord.IntegrationTests.Infrastructure
{
    public class DatabaseFixture : IAsyncLifetime
    {
        private static readonly string _defaultDatabaseName = $"{TestConstants.DatabaseNamePrefix}{Guid.NewGuid()}";
        private static string? DatabaseName { get; set; }

        public async Task InitializeAsync()
        {
            await DatabaseUtilities.DropOldDatabasesAsync(TestConstants.DatabaseNamePrefix);

            if (DatabaseName != null) return;

            DatabaseName = _defaultDatabaseName;

            await using var dataContext = GetContext(DatabaseUtilities.GetSqlConnectionString(DatabaseName));
            await dataContext.Database.MigrateAsync();
        }

        public static OverlordContext GetContext(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OverlordContext>();
            optionsBuilder.UseSqlServer(connectionString);

            var context = new OverlordContext(optionsBuilder.Options);

            return context;
        }

        public static string GetDatabaseName()
        {
            if (DatabaseName == null)
                throw new OperationCanceledException(
                    "The database name was accessed before it was initialized. Make sure not to access this property before the class is initialized");

            return DatabaseName;
        }

        public async Task DisposeAsync()
        {
            await DatabaseUtilities.KillAllConnectionsAndDropDatabase(DatabaseName!);
        }
    }
}
