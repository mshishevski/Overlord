using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Overlord.Application;
using Overlord.DataAccess;
using MediatR;

namespace Overlord.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOverlordServices(this IServiceCollection services, string connectionString)
        {
            return services
                .AddMediatR(configuration => configuration.RegisterServicesFromAssembly(ApplicationAssembly.Get()))
                .AddScoped<IOverlordContext, OverlordContext>()
                .AddDbContext<OverlordContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
