using System;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.Models;
using MediatR;

namespace DepthChartsManager.Core.UseCases.Player
{
    public class RemovePlayerCommand : IRequest<Models.Player>
    {
        public RemovePlayerCommand(RemovePlayerRequest removePlayerRequest)
        {
            RemovePlayerRequest = removePlayerRequest;
        }

        public RemovePlayerRequest RemovePlayerRequest { get; }
    }

    public class RemovePlayerCommandHandler : IRequestHandler<RemovePlayerCommand, Models.Player>
    {
        private readonly IMapper _mapper;
        public readonly IPlayerRepository _playerRepository;

        public RemovePlayerCommandHandler(IMapper mapper, IPlayerRepository playerRepository)
        {
            _mapper = mapper;
            _playerRepository = playerRepository;
        }

        public Task<Models.Player> Handle(RemovePlayerCommand request, CancellationToken cancellationToken)
        {
             
            //Get list of players from repository
            var players = _playerRepository.GetAllPlayers(new GetAllPlayersRequest { TeamId = request.RemovePlayerRequest.TeamId, LeagueId = request.RemovePlayerRequest.LeagueId }).ToList();
            if (!players.Any())
            {
                throw new PlayersNotFoundException(request.RemovePlayerRequest.LeagueId, request.RemovePlayerRequest.TeamId);
            }

            //find the player to be removed
            var player = players.Find(player => player.Id == request.RemovePlayerRequest.Id
                                                && string.Equals(request.RemovePlayerRequest.Name, player.Name, StringComparison.OrdinalIgnoreCase)
                                                && string.Equals(request.RemovePlayerRequest.Position, player.Position, StringComparison.OrdinalIgnoreCase))
                ?? throw new PlayerNotFoundException(request.RemovePlayerRequest.TeamId, request.RemovePlayerRequest.Position);

            //filter out the players to be updated
            var backupPlayers = players.Where(nextPlayer => nextPlayer.Position == player.Position && nextPlayer.PositionDepth > player.PositionDepth).ToList();

            //remove the player from players to repository
            _playerRepository.RemovePlayerFromDepthChart(player);

            //update the player positions to repository
            backupPlayers.ForEach(player => player.PositionDepth = player.PositionDepth - 1);
            _playerRepository.UpdatePlayerPositions(backupPlayers);

            return Task.FromResult(player);
        }
    }
}

