using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Overlord.DataAccess;

namespace Overlord.IntegrationTests.Infrastructure
{
    public static class FixtureHelper
    {
        internal static void RemoveDataContextRegistrations(IServiceCollection services)
        {
            services.RemoveAll(typeof(OverlordContext));
            services.RemoveAll(typeof(DbContextOptions<OverlordContext>));
        }

        internal static string GetConnectionString()
        {
            return DatabaseUtilities.GetSqlConnectionString(DatabaseFixture.GetDatabaseName());
        }
    }
}
