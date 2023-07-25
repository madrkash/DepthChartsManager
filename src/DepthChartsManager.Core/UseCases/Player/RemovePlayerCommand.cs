using System;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
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
        public readonly ISportRepository _sportRepository;

        public RemovePlayerCommandHandler(IMapper mapper, ISportRepository sportRepository)
        {
            _mapper = mapper;
            _sportRepository = sportRepository;
        }

        public Task<Models.Player> Handle(RemovePlayerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_sportRepository.RemovePlayerFromDepthChart(request.RemovePlayerRequest));
            }
            catch (Exception ex)
            {
                //TODO: Custom exception
                throw new Exception(ex.Message);
            }
        }
    }
}

