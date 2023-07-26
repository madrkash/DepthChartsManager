using DepthChartsManager.Common.Request;
using DepthChartsManager.ConsoleApp.Validators;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Core.UseCases.League;
using DepthChartsManager.Core.UseCases.Player;
using DepthChartsManager.Core.UseCases.Team;
using MediatR;
using ValidationException = DepthChartsManager.Core.Exceptions.ValidationException;

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
            var validator = new CreateLeagueRequestValidator();
            var result = await validator.ValidateAsync(createLeagueRequest);

            if (!result.IsValid)
            {
                throw new ValidationException(string.Join(",", result.Errors));
            }
            return await _mediator.Send(new AddLeagueCommand(createLeagueRequest));
            
        }

        public async Task<Team> AddTeam(CreateTeamRequest createTeamRequest)
        {
            var validator = new CreateTeamRequestValidator();
            var result = await validator.ValidateAsync(createTeamRequest);

            if (!result.IsValid)
            {
                throw new ValidationException(string.Join(",", result.Errors));
            }
            
            return await _mediator.Send(new AddTeamCommand(createTeamRequest));
             
        }

        public async Task<Player> AddPlayerToDepthChart(CreatePlayerRequest createPlayerRequest)
        {
            var validator = new CreatePlayerRequestValidator();
            var result = await validator.ValidateAsync(createPlayerRequest);

            if (!result.IsValid)
            {
                throw new ValidationException(string.Join(",", result.Errors));
            }
            return await _mediator.Send(new AddPlayerCommand(createPlayerRequest));
        }

        public async Task<Player> RemovePlayerFromDepthChart(RemovePlayerRequest removePlayerRequest)
        {
            var validator = new RemovePlayerRequestValidator();
            var result = await validator.ValidateAsync(removePlayerRequest);

            if (!result.IsValid)
            {
                throw new ValidationException(string.Join(",", result.Errors));
            }
            return await _mediator.Send(new RemovePlayerCommand(removePlayerRequest));
        }

        public async Task<IEnumerable<Player>> GetPlayerBackups(GetPlayerBackupsRequest getPlayerBackupsRequest)
        {
            var validator = new GetPlayerBackupsRequestValidator();
            var result = await validator.ValidateAsync(getPlayerBackupsRequest);

            if (!result.IsValid)
            {
                throw new ValidationException(string.Join(",", result.Errors));
            }
            return await _mediator.Send(new GetPlayerBackupsCommand(getPlayerBackupsRequest));
        }

        public async Task<IEnumerable<Player>> GetFullDepthChart(GetAllPlayersRequest getAllPlayersRequest)
        {
            var validator = new GetAllPlayersRequestValidator();
            var result = await validator.ValidateAsync(getAllPlayersRequest);

            if (!result.IsValid)
            {
                throw new ValidationException(string.Join(",", result.Errors));
            }
            return await _mediator.Send(new GetFullDepthChartCommand(getAllPlayersRequest));
        }
    }
}

