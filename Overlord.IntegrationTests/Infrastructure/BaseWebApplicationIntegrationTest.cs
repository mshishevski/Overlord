using Microsoft.Extensions.DependencyInjection;

namespace Overlord.IntegrationTests.Infrastructure
{
    public class BaseWebApplicationIntegrationTest : BaseIntegrationTest
    {
        protected readonly HttpClient _client;

        public BaseWebApplicationIntegrationTest(IServiceScope scope, HttpClient client, string connectionString) : base(scope, connectionString)
        {
            _client = client;
        }
    }
}
