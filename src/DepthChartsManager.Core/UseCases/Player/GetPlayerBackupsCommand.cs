using System;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.Models;
using MediatR;

namespace DepthChartsManager.Core.UseCases.Player
{
    public class GetPlayerBackupsCommand : IRequest<IEnumerable<Models.Player>>
    {
        public GetPlayerBackupsCommand(GetPlayerBackupsRequest getPlayerBackupsRequest)
        {
            GetPlayerBackupsRequest = getPlayerBackupsRequest;
        }

        public GetPlayerBackupsRequest GetPlayerBackupsRequest { get; }
    }

    public class GetPlayerBackupsCommandHandler : IRequestHandler<GetPlayerBackupsCommand, IEnumerable<Models.Player>>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetPlayerBackupsCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Task<IEnumerable<Models.Player>> Handle(GetPlayerBackupsCommand request, CancellationToken cancellationToken)
        {
            var players = _playerRepository.GetAllPlayers(new GetAllPlayersRequest { LeagueId = request.GetPlayerBackupsRequest.LeagueId, TeamId = request.GetPlayerBackupsRequest.TeamId }).ToList();

            if (!players.Any())
            {
                throw new PlayersNotFoundException(request.GetPlayerBackupsRequest.LeagueId, request.GetPlayerBackupsRequest.TeamId);
            }

            var player = players.Find(player => player.Id == request.GetPlayerBackupsRequest.PlayerId
                                                                   && string.Equals(request.GetPlayerBackupsRequest.Position, player.Position, StringComparison.OrdinalIgnoreCase));
            if(player == null)
            {
                return Task.FromResult(Enumerable.Empty<Models.Player>());
            }
            var playerPositionDepth = player.PositionDepth;

            var backupPlayers = players.Where(player => player.Position == request.GetPlayerBackupsRequest.Position && player.PositionDepth > playerPositionDepth);

            return Task.FromResult(backupPlayers.Any() ? backupPlayers : Enumerable.Empty<Models.Player>());

        }

       
    }
}

