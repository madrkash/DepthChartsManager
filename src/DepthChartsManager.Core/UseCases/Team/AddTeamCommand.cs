using System;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.UseCases.League;
using MediatR;

namespace DepthChartsManager.Core.UseCases.Team
{
    public class AddTeamCommand : IRequest<Models.Team>
    {
        public AddTeamCommand(CreateTeamRequest createTeamRequest)
        {
            CreateTeamRequest = createTeamRequest;
        }

        public CreateTeamRequest CreateTeamRequest { get; }
    }

    public class AddTeamCommandHandler : IRequestHandler<AddTeamCommand, Models.Team>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ILeagueRepository _leagueRepository;

        public AddTeamCommandHandler(ILeagueRepository leagueRepository, ITeamRepository teamRepository)
        {  
            _teamRepository = teamRepository;
            _leagueRepository = leagueRepository;
        }

        public Task<Models.Team> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {

            _ = _leagueRepository.GetLeague(request.CreateTeamRequest.LeagueId) ??
                throw new LeagueNotFoundException(request.CreateTeamRequest.LeagueId);

            var teams = _teamRepository.GetTeams(request.CreateTeamRequest.LeagueId).ToList();

            if(DoesTeamExist(request, teams))
            {
                throw new TeamAlreadyExistsException(request.CreateTeamRequest.TeamName);
            }

            return Task.FromResult(_teamRepository.AddTeam(new Models.Team
            {
                Id = request.CreateTeamRequest.Id,
                LeagueId = request.CreateTeamRequest.LeagueId,
                Name = request.CreateTeamRequest.TeamName
            }));
        }

        private static bool DoesTeamExist(AddTeamCommand request, List<Models.Team> teams) => teams.Any() && teams.Any(team => team.Name == request.CreateTeamRequest.TeamName);
    }
}


