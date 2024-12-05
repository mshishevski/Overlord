using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Overlord.IntegrationTests.Infrastructure;


namespace Overlord.IntegrationTests.Application.Infrastructure
{
    [Collection(CollectionNames.DatabaseDependentTest)]
    public abstract class BaseApplicationTest : BaseIntegrationTest, IClassFixture<OverlordApplicationTestFixture>
    {
        protected readonly OverlordApplicationTestFixture ApplicationTestFixture;
        protected readonly ISender _sender;
        protected BaseApplicationTest(OverlordApplicationTestFixture applicationTestFixture) : base(
            applicationTestFixture.Services.CreateScope(),
            FixtureHelper.GetConnectionString())
        {
            ApplicationTestFixture = applicationTestFixture;

            _sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        }
    }
}
