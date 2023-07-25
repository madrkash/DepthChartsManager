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
        private IServiceProvider _serviceProvider;

        private static void Main(string[] args)
        {
            Task.Run(async () => await new Program().ConfigureServices().Run()).Wait();
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
            throw new Exception("Kaboom");
        }

        private Program ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ILeagueRepository).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<ISportRepository, SportRepository>();
            services.AddSingleton<IDepthChartService, DepthChartService>();
            _serviceProvider = services.BuildServiceProvider();
            return this;
        }

        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine(e.ExceptionObject.ToString());
            Console.WriteLine("Press Enter to continue");
            Console.ReadLine();
            Environment.Exit(1);
        }

            private async Task Run()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();
            var mapper = _serviceProvider.GetService<IMapper>();

            // Add league(s)
            var nfl = await depthChartService.AddLeague(new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build()); //NFL
            var mlb = await depthChartService.AddLeague(new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("MLB")
                .Build()); //MLB

            var tampaBayBuccaneers = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamName("Tampa Bay Buccaneers")
                .Build());     
            var buffaloBills = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamName("Buffalo Bills")
                .Build());  
            var miamiDolphins = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamName("Miami Dolphins")
                .Build());


            // Add players for NFL
            var tomBrady = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .Build());
            var blaineGabbert = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Blaine Gabbert")
                .WithPosition(NFLPositions.LWR)
                .Build());
            var kyleTrask = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Kyle Trask")
                .WithPosition(NFLPositions.LWR)
                .Build());
            var mikeEvans = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Mike Evans")
                .WithPosition(NFLPositions.QB)
                .Build()); 
            var jaelonDarden = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Jaelon Darden")
                .WithPosition(NFLPositions.QB)
                .Build());
            var scottMiller = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Scott Miller")
                .WithPosition(NFLPositions.QB)
                .Build());

            // Get backups for NFL
            var tomBradyBackups = await depthChartService.GetPlayerBackups(mapper.Map<GetPlayerBackupsRequest>(tomBrady));

            if (tomBradyBackups != null)
            {
                foreach (var backupPlayerPosition in tomBradyBackups)
                {
                    Console.WriteLine($"#{backupPlayerPosition.Id} - {backupPlayerPosition.Name}");
                }
            }
            var removePlayer = await depthChartService.RemovePlayerFromDepthChart(mapper.Map<RemovePlayerRequest>(tomBrady));
            if (removePlayer != null) Console.WriteLine($"\n#{removePlayer.Id} - {removePlayer.Name}\n");

            var fullDepthChart = await depthChartService.GetFullDepthChart(new GetFullDepthChartRequestBuilder()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .Build());

            if (fullDepthChart != null)
            {
                foreach (var group in fullDepthChart.GroupBy(p => p.Position))
                {
                    Console.WriteLine($"{group.Key} -{string.Join(",", group.Select(s => $" (#{s.Id}, {s.Name})"))}");
                }
            }



            Console.ReadLine();

        }
    }

}

