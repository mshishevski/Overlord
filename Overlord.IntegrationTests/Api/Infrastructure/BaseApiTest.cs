using Microsoft.Extensions.DependencyInjection;

using Overlord.IntegrationTests.Infrastructure;

namespace Overlord.IntegrationTests.Api.Infrastructure
{
    [Collection(CollectionNames.DatabaseDependentTest)]
    public abstract class BaseApiTest : BaseWebApplicationIntegrationTest, IClassFixture<OverlordApiTestFixture>
    {
        protected readonly OverlordApiTestFixture _apiTestFixture;

        protected BaseApiTest(OverlordApiTestFixture apiTestFixture) : base(
            apiTestFixture.Services.CreateScope(), apiTestFixture.CreateClient(), FixtureHelper.GetConnectionString())
        {
            _apiTestFixture = apiTestFixture;
        }


    }
}
