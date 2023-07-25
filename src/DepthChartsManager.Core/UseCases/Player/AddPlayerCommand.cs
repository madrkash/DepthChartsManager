using System;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using MediatR;

namespace DepthChartsManager.Core.UseCases.Player
{
    public class AddPlayerCommand : IRequest<Models.Player>
    {
        public AddPlayerCommand(CreatePlayerRequest createPlayerRequest)
        {
            AddPlayerRequest = createPlayerRequest;
        }

        public CreatePlayerRequest AddPlayerRequest { get; }
    }

    public class AddPlayerCommandHandler : IRequestHandler<AddPlayerCommand, Models.Player>
    {
        public readonly ISportRepository _sportRepository;

        public AddPlayerCommandHandler(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }

        public Task<Models.Player> Handle(AddPlayerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_sportRepository.AddPlayerToDepthChart(request.AddPlayerRequest));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

