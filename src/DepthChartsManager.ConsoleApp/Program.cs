using System.Reflection;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Services;
using DepthChartsManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DepthChartsManager.ConsoleApp
{
    public class Program
    {
        
        private static async Task Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var logger = serviceProvider.GetRequiredService<ILogger>();
            try
            {   
                var depthChartsClient = serviceProvider.GetRequiredService<DepthChartsClient>();
                await depthChartsClient.Process();
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddLogging(builder =>
            {
                var logger = new LoggerConfiguration()
                              .MinimumLevel.Information()
                              .WriteTo.Console()
                              .CreateLogger();
                builder.AddSerilog(logger);
            });
                   
            services.AddSingleton<ILogger>(sp =>
            {
                return new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .CreateLogger();
            });
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ILeagueRepository).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<ILeagueRepository, LeagueRepository>();
            services.AddSingleton<ITeamRepository, TeamRepository>();
            services.AddSingleton<IPlayerRepository, PlayerRepository>();
            services.AddSingleton<IDepthChartService, DepthChartService>();
            services.AddSingleton<DepthChartsClient>();
            return services.BuildServiceProvider();

           
        }

         

      
    }

}

