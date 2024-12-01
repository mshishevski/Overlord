using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Overlord.Infrastructure.DependencyInjection;
using Overlord.IntegrationTests.Infrastructure;

namespace Overlord.IntegrationTests.Application.Infrastructure
{
    [CollectionDefinition(CollectionNames.DatabaseDependentTest)]
    public class OverlordApplicationTestFixture : ICollectionFixture<DatabaseFixture>
    {
        protected readonly DatabaseFixture _databaseFixture;
        public OverlordApplicationTestFixture(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;

            var services = new ServiceCollection()
                .AddOverlordServices(FixtureHelper.GetConnectionString());

            AddConfigurations(services);

            Services = services
                .BuildServiceProvider();
        }


        private void AddConfigurations(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }

        public IServiceProvider Services { get; private set; }
    }
}
