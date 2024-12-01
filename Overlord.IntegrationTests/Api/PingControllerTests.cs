using Overlord.IntegrationTests.Api.Infrastructure;
using Overlord.IntegrationTests.Externsions;

namespace Overlord.IntegrationTests.Api
{
    public class PingControllerTests : BaseApiTest
    {
        private const string _pingEndpoint = "api/ping";

        public PingControllerTests(OverlordApiTestFixture apiTestFixture) : base(apiTestFixture)
        {

        }

        [Fact]
        public async Task It_is_possible_to_ping_endpoint()
        {
            var client = _apiTestFixture.CreateAnonymousClient();

            var result = await client.GetAsync(_pingEndpoint);
            result.AssertStatusCodeSuccess();

        }

    }
}
