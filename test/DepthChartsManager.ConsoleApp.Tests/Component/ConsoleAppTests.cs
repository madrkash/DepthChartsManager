using System;
using System.Reflection;
using AutoMapper;
using DepthChartsManager.Common.Constants;
using DepthChartsManager.ConsoleApp.Builders;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Services;
using DepthChartsManager.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DepthChartsManager.ConsoleApp.Tests.Component
{
    public class ConsoleAppTests
    {
        private readonly ServiceProvider _serviceProvider;

        public ConsoleAppTests()
		{
            var services = new ServiceCollection();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ILeagueRepository).Assembly));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSingleton<ISportRepository, SportRepository>();
            services.AddSingleton<IDepthChartService, DepthChartService>();
            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public async void WhenApplicationRuns_AddPlayerToDepthChartAtAGivenPosition_IsSuccessful()
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

            Assert.NotNull(tomBrady);
            Assert.Equal(0, tomBrady.PositionDepth);
            Assert.Equal(1, blaineGabbert.PositionDepth);
            Assert.Equal(2, kyleTrask.PositionDepth);
        }


        [Fact]
        public async void WhenApplicationRuns_NewTeamIsCreated()
        {
            var depthChartService = _serviceProvider.GetService<IDepthChartService>();
            var mapper = _serviceProvider.GetService<IMapper>();

            var createLeagueRequest = new CreateLeagueRequestBuilder()
                .WithDefaultValues()
                .WithName("NFL")
                .Build();
          

            // Add league(s)
            var nfl = await depthChartService.AddLeague(createLeagueRequest); //NFL

            var createTeamRequest = new CreateTeamRequestBuilder()
             .WithDefaultValues()
             .WithLeagueId(nfl.Id)
             .WithTeamName("Buffalo Bills")
             .Build();

            var buffaloBills = await depthChartService.AddTeam(createTeamRequest);

            Assert.NotNull(buffaloBills);
            Assert.Equal(buffaloBills.Id, createTeamRequest.Id);
            Assert.Equal(buffaloBills.Name, createTeamRequest.TeamName);
        }


    }
}

