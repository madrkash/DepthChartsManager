using System;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
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
        private readonly ISportRepository _sportRepository;

        public GetPlayerBackupsCommandHandler(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }

        public Task<IEnumerable<Models.Player>> Handle(GetPlayerBackupsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_sportRepository.GetBackups(request.GetPlayerBackupsRequest));
            }
            catch (Exception ex)
            {
                //TODO: Custom exception
                throw new Exception(ex.Message);
            }
        }
    }
}

