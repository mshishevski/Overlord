using Overlord.IntegrationTests.Api.Infrastructure;
using Overlord.IntegrationTests.Externsions;

namespace Overlord.IntegrationTests.Api
{
    public class SwaggerTests : BaseApiTest
    {
        private const string _swaggerUiEndpoint = "/swagger/index.html";
        public SwaggerTests(OverlordApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public async Task It_is_possible_to_access_swagger_as_an_anonymous_user()
        {
            var client = _apiTestFixture.CreateAnonymousClient();
            var result = await client.GetAsync(_swaggerUiEndpoint);

            result.AssertStatusCodeSuccess();
        }
    }
}
