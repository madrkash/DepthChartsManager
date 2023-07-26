using System.Reflection;
using AutoMapper;
using DepthChartsManager.Common.Constants;
using DepthChartsManager.Common.Request;
using DepthChartsManager.ConsoleApp.Builders;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Services;
using DepthChartsManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DepthChartsManager.ConsoleApp
{
    public class Program
    {
        
        private static async void Main(string[] args)
        {
            try
            {
                var serviceProvider = ConfigureServices();
                var depthChartsClient = serviceProvider.GetRequiredService<DepthChartsClient>();
                await depthChartsClient.Process();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ILeagueRepository).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<ISportRepository, SportRepository>();
            services.AddSingleton<IDepthChartService, DepthChartService>();
            services.AddSingleton<DepthChartsClient>();
            return services.BuildServiceProvider();
        }

         

      
    }

}

