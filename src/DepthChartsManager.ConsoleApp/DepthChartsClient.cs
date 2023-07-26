using AutoMapper;
using DepthChartsManager.Common.Constants;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Common.Builders;
using DepthChartsManager.Core.Contracts;
using Microsoft.Extensions.Logging;

namespace DepthChartsManager.ConsoleApp
{
    public class DepthChartsClient
	{
        private readonly IDepthChartService _depthChartService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepthChartsClient> _logger;

        public DepthChartsClient(IDepthChartService depthChartService, IMapper mapper, ILogger<DepthChartsClient> logger)
        {
            _depthChartService = depthChartService;
            _mapper = mapper;
            _logger = logger;
        }
        
        public async Task Process()
        {
            // Add league(s)
            var nfl = await _depthChartService.AddLeague(new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build()); //NFL
            var mlb = await _depthChartService.AddLeague(new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("MLB")
                .Build()); //MLB

            //Add teams to NFL
            var tampaBayBuccaneers = await _depthChartService.AddTeam(new CreateTeamRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamName("Tampa Bay Buccaneers")
                .Build());
            var buffaloBills = await _depthChartService.AddTeam(new CreateTeamRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamName("Buffalo Bills")
                .Build());
            var miamiDolphins = await _depthChartService.AddTeam(new CreateTeamRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamName("Miami Dolphins")
                .Build());


            // Add players for NFL
            var tomBrady = await _depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .Build());
            var blaineGabbert = await _depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Blaine Gabbert")
                .WithPosition(NFLPositions.LWR)
                .Build());
            var kyleTrask = await _depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Kyle Trask")
                .WithPosition(NFLPositions.LWR)
                .Build());
            var mikeEvans = await _depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Mike Evans")
                .WithPosition(NFLPositions.QB)
                .Build());
            var jaelonDarden = await _depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Jaelon Darden")
                .WithPosition(NFLPositions.QB)
                .Build());
            var scottMiller = await _depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                 .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Scott Miller")
                .WithPosition(NFLPositions.QB)
                .Build());

            // Get backups for NFL
            var tomBradyBackups = await _depthChartService.GetPlayerBackups(_mapper.Map<GetPlayerBackupsRequest>(tomBrady));

            if (tomBradyBackups != null)
            {
                foreach (var backupPlayerPosition in tomBradyBackups)
                {
                    _logger.LogInformation($"#{backupPlayerPosition.Id} - {backupPlayerPosition.Name}");
                }
            }
            var removePlayer = await _depthChartService.RemovePlayerFromDepthChart(_mapper.Map<RemovePlayerRequest>(tomBrady));
            if (removePlayer != null) _logger.LogInformation($"\n#{removePlayer.Id} - {removePlayer.Name}\n");

            var fullDepthChart = await _depthChartService.GetFullDepthChart(new GetAllPlayersRequestBuilder()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .Build());

            if (fullDepthChart != null)
            {
                foreach (var group in fullDepthChart.GroupBy(p => p.Position))
                {
                    _logger.LogInformation($"{group.Key} -{string.Join(",", group.Select(s => $" (#{s.Id}, {s.Name})"))}");
                }
            }
            Console.ReadLine();
        }
    }
}

