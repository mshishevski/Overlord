using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Overlord.IntegrationTests.Api.Infrastructure;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Migrations.Design;
using Microsoft.EntityFrameworkCore.SqlServer.Design.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Overlord.IntegrationTests.DataContext
{
    public class OverlordDbContextTests : BaseApiTest
    {
        public OverlordDbContextTests(OverlordApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        [SuppressMessage("Usage", "EF1001", MessageId = "Internal EF Core API usage.")]
        public void Scaffolding_a_new_migration_must_return_an_empty_migration()
        {
            var migrationName = $"ScaffoldedMigration{Guid.NewGuid()}";
            var rootNamespace = "Overlord.DataAccess";
            var migrationCodeRegex = new Regex(@"void Up\(MigrationBuilder migrationBuilder\).*?\{(.*?)\}", RegexOptions.Singleline);

            using var dbContext = GetDbContext();

            var services = new ServiceCollection()
                .AddEntityFrameworkDesignTimeServices()
                .AddDbContextDesignTimeServices(dbContext);

            var designTimeServices = new SqlServerDesignTimeServices();
            designTimeServices.ConfigureDesignTimeServices(services);

            var serviceProvider = services.BuildServiceProvider();

            // Scaffold a new migration
            var migrationsScaffolder = serviceProvider.GetRequiredService<IMigrationsScaffolder>();
            var scaffoldedMigration = migrationsScaffolder.ScaffoldMigration(migrationName, rootNamespace);

            var match = migrationCodeRegex.Match(scaffoldedMigration.MigrationCode);

            Assert.True(match.Success);
            var migrationUpGroup = match.Groups[1];
            Assert.True(migrationUpGroup.Success);
            Assert.True(string.IsNullOrWhiteSpace(migrationUpGroup.Value),
                $"A non-empty migration can be scaffolded. Make sure to add any missing migrations before integrating this branch.\nScaffolded migration:\n\n{scaffoldedMigration.MigrationCode}");
        }
    }
}
