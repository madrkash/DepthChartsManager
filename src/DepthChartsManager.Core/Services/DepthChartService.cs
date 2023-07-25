using System;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.UseCases.League;
using DepthChartsManager.Core.UseCases.Player;
using DepthChartsManager.Core.UseCases.Team;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Common.Request;
using MediatR;

namespace DepthChartsManager.Core.Services
{

    public class DepthChartService : IDepthChartService
    {
        private readonly IMediator _mediator;
        public DepthChartService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<League> AddLeague(CreateLeagueRequest createLeagueRequest)
        {
            //Fail fast
            return await _mediator.Send(new AddLeagueCommand(createLeagueRequest));
        }

        public async Task<Team> AddTeam(CreateTeamRequest createTeamRequest)
        {
            return await _mediator.Send(new AddTeamCommand(createTeamRequest));
        }

        public async Task<Player> AddPlayerToDepthChart(CreatePlayerRequest createPlayerRequest)
        {
            return await _mediator.Send(new AddPlayerCommand(createPlayerRequest));
        }

        public async Task<Player> RemovePlayerFromDepthChart(RemovePlayerRequest removePlayerRequest)
        {
            return await _mediator.Send(new RemovePlayerCommand(removePlayerRequest));
        }

        public async Task<IEnumerable<Player>> GetPlayerBackups(GetPlayerBackupsRequest getPlayerBackupsRequest)
        {
            return await _mediator.Send(new GetPlayerBackupsCommand(getPlayerBackupsRequest));
        }

        public async Task<IEnumerable<Player>> GetFullDepthChart(GetFullDepthChartRequest getFullDepthChartRequest)
        {
            return await _mediator.Send(new GetFullDepthChartCommand(getFullDepthChartRequest));
        }
    }
}

