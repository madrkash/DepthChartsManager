using System;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.Models;
using MediatR;

namespace DepthChartsManager.Core.UseCases.Player
{
    public class AddPlayerCommand : IRequest<Models.Player>
    {
        public AddPlayerCommand(CreatePlayerRequest createPlayerRequest)
        {
            CreatePlayerRequest = createPlayerRequest;
        }

        public CreatePlayerRequest CreatePlayerRequest { get; }
    }

    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, Models.Player>
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IPlayerRepository _playerRepository;

        public AddPlayerCommandHandler(ILeagueRepository leagueRepository, ITeamRepository teamRepository, IPlayerRepository playerRepository)
        {
            _leagueRepository = leagueRepository;
            _teamRepository = teamRepository;
            _playerRepository = playerRepository;
        }

        public Task<Models.Player> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
        {
           
                _ = _leagueRepository.GetLeague(request.CreatePlayerRequest.LeagueId) ??
               throw new LeagueNotFoundException(request.CreatePlayerRequest.LeagueId);

                var teams = _teamRepository.GetTeams(request.CreatePlayerRequest.LeagueId).ToList();
                if (!teams.Exists(team => team.Id == request.CreatePlayerRequest.TeamId))
                {
                    throw new TeamNotFoundException(request.CreatePlayerRequest.LeagueId, request.CreatePlayerRequest.TeamId);
                }

                var players = _playerRepository.GetAllPlayers(new GetAllPlayersRequest { TeamId = request.CreatePlayerRequest.TeamId, LeagueId = request.CreatePlayerRequest.LeagueId }).ToList();
                if (DoesPlayerExist(request, players))
                {
                    throw new PlayerAlreadyExistsException(request.CreatePlayerRequest.Name, request.CreatePlayerRequest.Position);
                }

                int index = CalculatePositionDepth(players, request.CreatePlayerRequest);

                UpdateBackupPlayerPositions(request, players, index);

                return Task.FromResult(_playerRepository.AddPlayerToDepthChart(new Models.Player
                {
                    Id = request.CreatePlayerRequest.Id,
                    LeagueId = request.CreatePlayerRequest.LeagueId,
                    TeamId = request.CreatePlayerRequest.TeamId,
                    Name = request.CreatePlayerRequest.Name,
                    Position = request.CreatePlayerRequest.Position,
                    PositionDepth = index
                }));
          
        }

        private void UpdateBackupPlayerPositions(AddPlayerCommand request, List<Models.Player> players, int index)
        {
            if (request.CreatePlayerRequest.PositionDepth != null)
            {
                var backupPlayers = players.Where(nextPlayer => nextPlayer.PositionDepth >= index && nextPlayer.Position == request.CreatePlayerRequest.Position).ToList();

                if (backupPlayers.Any())
                {
                    backupPlayers.ForEach(player => player.PositionDepth = player.PositionDepth + 1);
                    _playerRepository.UpdatePlayerPositions(backupPlayers);
                }
            }
        }

        private bool DoesPlayerExist(AddPlayerCommand request, List<Models.Player> players)
        {
            return players.Exists(player =>
            string.Equals(player.Name, request.CreatePlayerRequest.Name, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(player.Position, request.CreatePlayerRequest.Position, StringComparison.OrdinalIgnoreCase));
        }

        private int CalculatePositionDepth(List<Models.Player> players, CreatePlayerRequest createPlayerRequest)
        {
            if (createPlayerRequest.PositionDepth == null)
            {
                if (players.Any())
                {
                    var existingPlayers = players.Where(x => x.Position == createPlayerRequest.Position).OrderBy(x => x.PositionDepth).ToList();
                    if (existingPlayers.Any())
                    {
                        return (int)(existingPlayers.Last().PositionDepth + 1);
                    }
                }
               return 0;
            }

            return (int)createPlayerRequest.PositionDepth;
        }
    }
}

