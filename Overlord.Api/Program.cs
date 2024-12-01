using Overlord.Api.Extensions;
using Overlord.Domain.Options;
using Overlord.Infrastructure.DependencyInjection;

namespace Overlord.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.Configure<MqttOptions>(builder.Configuration.GetSection(nameof(MqttOptions)));

            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ConfigureHttpsDefaults(options =>
                {
                    // TODO
                });
            });

            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new SlugifyRouteConvention());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMqttBrokerServices();

            var connectionString = builder.Configuration.GetConnectionString("OverlordContext");
            builder.Services.AddOverlordServices(connectionString!);

            WebApplication? app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.Run();
        }
    }

    public partial class ProgramApi { }
}
