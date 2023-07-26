using System;
using AutoMapper;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Common.Request;
using MediatR;

namespace DepthChartsManager.Core.UseCases.Player
{
    public class GetFullDepthChartCommand : IRequest<IEnumerable<Models.Player>>
    {
        public GetFullDepthChartCommand(GetAllPlayersRequest getFullDepthChartRequest)
        {
            GetFullDepthChartRequest = getFullDepthChartRequest;
        }

        public GetAllPlayersRequest GetFullDepthChartRequest { get; }
    }

    public class GetFullDepthChartCommandHandler : IRequestHandler<GetFullDepthChartCommand, IEnumerable<Models.Player>>
    {
        private readonly IPlayerRepository _playerRepository;

        public GetFullDepthChartCommandHandler(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public Task<IEnumerable<Models.Player>> Handle(GetFullDepthChartCommand request, CancellationToken cancellationToken)
        {
             return Task.FromResult(_playerRepository.GetAllPlayers(request.GetFullDepthChartRequest));
        }
    }
}

