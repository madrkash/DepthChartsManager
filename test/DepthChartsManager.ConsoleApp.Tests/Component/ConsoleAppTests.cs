using System;
using System.Reflection;
using AutoMapper;
using DepthChartsManager.Common.Constants;
using DepthChartsManager.Common.Builders;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Services;
using DepthChartsManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.UseCases.Player;

namespace DepthChartsManager.ConsoleApp.Tests.Component
{
    public class ConsoleAppTests
    {
        private readonly IServiceProvider _serviceProvider;

        public ConsoleAppTests()
		{
            _serviceProvider = Program.ConfigureServices();
        }

        [Fact]
        public async void WhenAddingPlayerToDepthChart_GivenPositionDepth_AddsPlayerAndMovesDownBelowPlayers()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();
            
            var createLeagueRequest = new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build();

            // Add league(s)
            var nfl = await depthChartService.AddLeague(createLeagueRequest); //NFL


            var tampaBayBuccaneers = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
               .WithDefaultValues()
               .WithLeagueId(nfl.Id)
               .WithTeamName("Tampa Bay Buccaneers")
               .Build());

            // Add players for NFL
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

            //Add Tom Brady to the top of the position depth
            var tomBrady = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .WithPositionDepth(0)
                .Build());

            Assert.NotNull(tomBrady);
            Assert.Equal(0, tomBrady.PositionDepth);
            Assert.Equal(1, blaineGabbert.PositionDepth);
            Assert.Equal(2, kyleTrask.PositionDepth);
        }

        [Fact]
        public async void WhenAddingPlayerToDepthChart_GivenNoPositionDepth_IsAddedAtTheEnd()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();
            
            var createLeagueRequest = new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build();

            // Add league(s)
            var nfl = await depthChartService.AddLeague(createLeagueRequest); //NFL


            var tampaBayBuccaneers = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
               .WithDefaultValues()
               .WithLeagueId(nfl.Id)
               .WithTeamName("Tampa Bay Buccaneers")
               .Build());

            // Add players for NFL
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

            //Add Tom Brady to the bottom of the position depth
            var tomBrady = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .Build());

            Assert.NotNull(tomBrady);
            Assert.Equal(0, blaineGabbert.PositionDepth);
            Assert.Equal(1, kyleTrask.PositionDepth);
            Assert.Equal(2, tomBrady.PositionDepth);
        }

        [Fact]
        public async void WhenRemovingPlayerFromDepthChart_GivenPosition_IsRemovedSuccessfully()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();
            
            var createLeagueRequest = new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build();

            // Add league(s)
            var nfl = await depthChartService.AddLeague(createLeagueRequest); //NFL


            var tampaBayBuccaneers = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
               .WithDefaultValues()
               .WithLeagueId(nfl.Id)
               .WithTeamName("Tampa Bay Buccaneers")
               .Build());

            // Add players for NFL
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

            //Add Tom Brady to the top of the position depth
            var tomBrady = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .WithPositionDepth(0)
                .Build());

            var tomBradyRemoved = await depthChartService.RemovePlayerFromDepthChart(new RemovePlayerRequestBuilder()
                .WithId(tomBrady.Id)
                .WithLeagueId(tomBrady.LeagueId)
                .WithName(tomBrady.Name)
                .WithPosition(tomBrady.Position)
                .WithTeamId(tomBrady.TeamId)
                .Build());

            var players = await depthChartService.GetFullDepthChart(new GetAllPlayersRequestBuilder()
                .WithLeagueId(tomBrady.LeagueId)
                .WithTeamId(tomBrady.TeamId)
                .Build());


            Assert.NotNull(tomBradyRemoved);
            Assert.Equal(2, players.Count());
            Assert.Equal(0, blaineGabbert.PositionDepth);
            Assert.Equal(1, kyleTrask.PositionDepth);
        }

        [Fact]
        public async void WhenRemovingPlayerFromDepthChart_GivenPlayerNotAvailableAtPosition_ThrowsPlayerNotFoundException()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();

            var createLeagueRequest = new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build();

            // Add league(s)
            var nfl = await depthChartService.AddLeague(createLeagueRequest); //NFL


            var tampaBayBuccaneers = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
               .WithDefaultValues()
               .WithLeagueId(nfl.Id)
               .WithTeamName("Tampa Bay Buccaneers")
               .Build());

            // Add players for NFL
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

            //Add Tom Brady to the top of the position depth
            var tomBrady = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .WithPositionDepth(0)
                .Build());
           
            // Act & Assert
            await Assert.ThrowsAsync<PlayerNotFoundException>(() => depthChartService.RemovePlayerFromDepthChart(new RemovePlayerRequestBuilder()
                .WithId(tomBrady.Id)
                .WithLeagueId(tomBrady.LeagueId)
            .WithName(tomBrady.Name)
            .WithPosition(NFLPositions.LT)
            .WithTeamId(tomBrady.TeamId)
                .Build())); }

        [Fact]
        public async void WhenGettingBackupPlayers_GivenValidPlayer_ReturnsAllBackupPlayers()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();
            var mapper = _serviceProvider.GetService<IMapper>();

            var createLeagueRequest = new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build();
          

            // Add league(s)
            var nfl = await depthChartService.AddLeague(createLeagueRequest); //NFL

            var tampaBayBuccaneers = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
               .WithDefaultValues()
               .WithLeagueId(nfl.Id)
               .WithTeamName("Tampa Bay Buccaneers")
               .Build());

            // Add players for NFL
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

            //Add Tom Brady to the top of the position depth
            var tomBrady = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .WithPositionDepth(0)
                .Build());

            var backupPlayers = await depthChartService.GetPlayerBackups(new GetPlayerBackupsRequestBuilder()
                .WithLeagueId(tomBrady.LeagueId)
                .WithName(tomBrady.Name)
                .WithPlayerId(tomBrady.Id)
                .WithPosition(tomBrady.Position)
                .WithTeamId(tomBrady.TeamId)
                .Build());

            Assert.NotEmpty(backupPlayers);
            Assert.Equal(2, backupPlayers.Count());
            Assert.Equal(blaineGabbert.Name, backupPlayers.ToList()[0].Name);
            Assert.Equal(kyleTrask.Name, backupPlayers.ToList()[1].Name);
        }

        [Fact]
        public async void WhenGettingBackupPlayers_GivenPlayerWithNoBackups_ReturnsEmptyList()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();
            var mapper = _serviceProvider.GetService<IMapper>();

            var createLeagueRequest = new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build();


            // Add league(s)
            var nfl = await depthChartService.AddLeague(createLeagueRequest); //NFL

            var tampaBayBuccaneers = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
               .WithDefaultValues()
               .WithLeagueId(nfl.Id)
               .WithTeamName("Tampa Bay Buccaneers")
               .Build());

            // Add players for NFL
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

            //Add Tom Brady to the top of the position depth
            var tomBrady = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.QB)
                .WithPositionDepth(0)
                .Build());

            var backupPlayers = await depthChartService.GetPlayerBackups(new GetPlayerBackupsRequestBuilder()
                .WithLeagueId(tomBrady.LeagueId)
                .WithName(tomBrady.Name)
                .WithPlayerId(tomBrady.Id)
                .WithPosition(tomBrady.Position)
                .WithTeamId(tomBrady.TeamId)
                .Build());

            Assert.Empty(backupPlayers);
        }

        [Fact]
        public async void WhenGettingBackupPlayers_GivenPlayerWithIncorrectPosition_ReturnsEmptyList()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();

            var createLeagueRequest = new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build();

            // Add league(s)
            var nfl = await depthChartService.AddLeague(createLeagueRequest); //NFL

            var tampaBayBuccaneers = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
               .WithDefaultValues()
               .WithLeagueId(nfl.Id)
               .WithTeamName("Tampa Bay Buccaneers")
               .Build());

            // Add players for NFL
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

            //Add Tom Brady to the top of the position depth
            var tomBrady = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .WithPositionDepth(0)
                .Build());

            var backupPlayers = await depthChartService.GetPlayerBackups(new GetPlayerBackupsRequestBuilder()
                .WithLeagueId(tomBrady.LeagueId)
                .WithName(tomBrady.Name)
                .WithPlayerId(tomBrady.Id)
                .WithPosition(NFLPositions.RB)
                .WithTeamId(tomBrady.TeamId)
                .Build());

            Assert.Empty(backupPlayers);
        }

        [Fact]
        public async void WhenGettingFullDepthChart_GivenValidData_ReturnsFullDepthChart()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();

            var createLeagueRequest = new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build();


            // Add league(s)
            var nfl = await depthChartService.AddLeague(createLeagueRequest); //NFL

            var tampaBayBuccaneers = await depthChartService.AddTeam(new CreateTeamRequestBuilder()
               .WithDefaultValues()
               .WithLeagueId(nfl.Id)
               .WithTeamName("Tampa Bay Buccaneers")
               .Build());

            // Add players for NFL
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

            //Add Tom Brady to the top of the position depth
            var tomBrady = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(nfl.Id)
                .WithTeamId(tampaBayBuccaneers.Id)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .WithPositionDepth(0)
                .Build());

            var tonyStark = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
            .WithDefaultValues()
            .WithLeagueId(nfl.Id)
             .WithTeamId(tampaBayBuccaneers.Id)
            .WithName("Tony Stark")
            .WithPosition(NFLPositions.QB)
            .Build());

            var dwayneJohnson = await depthChartService.AddPlayerToDepthChart(new CreatePlayerRequestBuilder()
          .WithDefaultValues()
          .WithLeagueId(nfl.Id)
           .WithTeamId(tampaBayBuccaneers.Id)
          .WithName("Dwayne Johnson")
          .WithPosition(NFLPositions.QB)
          .Build());

            var allPlayers = await depthChartService.GetFullDepthChart(new GetAllPlayersRequestBuilder()
                .WithLeagueId(tomBrady.LeagueId)
                .WithTeamId(tomBrady.TeamId)
                .Build());

            Assert.NotEmpty(allPlayers);
            Assert.Equal(5, allPlayers.Count());
        }
    }
}

