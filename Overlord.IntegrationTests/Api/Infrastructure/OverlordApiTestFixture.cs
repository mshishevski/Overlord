using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Overlord.IntegrationTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Overlord.Api;

namespace Overlord.IntegrationTests.Api.Infrastructure
{
    [CollectionDefinition(CollectionNames.DatabaseDependentTest)]
    public class OverlordApiTestFixture : BaseApiTestFixture<ProgramApi>, ICollectionFixture<DatabaseFixture>
    {
        public OverlordApiTestFixture(DatabaseFixture databaseFixture) : base(databaseFixture)
        {
            
        }


        protected override void ConfigureWebHostContext(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IStartupFilter>(new CertificateLoaderConfiguration());
            });
        }

        public new HttpClient CreateClient() => base.CreateClient();

        public HttpClient CreateAnonymousClient() => base.CreateClient();

        private sealed class CertificateLoaderConfiguration : IStartupFilter
        {
            public CertificateLoaderConfiguration()
            {
            }

            public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            {
                return builder =>
                {
                    builder.Use((ctx, nxt) =>
                    {
                        return nxt();
                    });
                    next(builder);
                };
            }
        }
    }
}
