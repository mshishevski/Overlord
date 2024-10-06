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
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMqttBrokerServices();

            string? connectionString = builder.Configuration.GetConnectionString("OverlordContext");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("EMPTY CONNECTION STRING!");
            }

            builder.Services.AddOverlordServices(connectionString);

            //builder.WebHost.UseKestrel(options =>
            //{
            //    options.
            //});

            var app = builder.Build();
            
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.Run();
        }
    }
}
