using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using Overlord.DataAccess;
using Overlord.Domain;

namespace Overlord.IntegrationTests.Infrastructure
{
    public abstract class BaseApiTestFixture<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected readonly DatabaseFixture _databaseFixture;

        public BaseApiTestFixture(DatabaseFixture databaseFixture)
        {
            _databaseFixture = databaseFixture;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.BuildServiceProvider(new ServiceProviderOptions()
                {
                    ValidateOnBuild = true,
                    ValidateScopes = true
                });
                FixtureHelper.RemoveDataContextRegistrations(services);
                services.AddDbContext<OverlordContext>(options => options
                    .UseSqlServer(FixtureHelper.GetConnectionString()));
            })
            .UseEnvironment(Environments.IntegrationTest);

            ConfigureWebHostContext(builder);
        }

        protected abstract void ConfigureWebHostContext(IWebHostBuilder builder);
    }
}
