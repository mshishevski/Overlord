using Microsoft.Extensions.DependencyInjection;

using Overlord.DataAccess;

namespace Overlord.IntegrationTests.Infrastructure
{
    public abstract class BaseIntegrationTest : IDisposable
    {
        private readonly string _connectionString;
        protected readonly IServiceScope _scope;

        protected readonly OverlordContext InitialDbContext;

        public BaseIntegrationTest(IServiceScope scope, string connectionString)
        {
            _scope = scope;
            _connectionString = connectionString;
            InitialDbContext = GetDbContext();

            InitialDbContext = GetDbContext();
        }

        protected OverlordContext GetDbContext()
        {
            return DatabaseFixture.GetContext(_connectionString);
        }


        public void Dispose()
        {
            GC.SuppressFinalize(this);
            _scope.Dispose();
        }

    }
}
