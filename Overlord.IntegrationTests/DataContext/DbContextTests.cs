using Overlord.IntegrationTests.Api.Infrastructure;

namespace Overlord.IntegrationTests.DataContext
{
    public class DbContextTests : BaseApiTest
    {
        public DbContextTests(OverlordApiTestFixture apiTestFixture) : base(apiTestFixture)
        {
        }

        [Fact]
        public void Database_is_successfully_created()
        {
            var dbContext = GetDbContext();

            Assert.True(dbContext != default);
        }
    }
}
