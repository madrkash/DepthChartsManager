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
        public GetFullDepthChartCommand(GetFullDepthChartRequest getFullDepthChartRequest)
        {
            GetFullDepthChartRequest = getFullDepthChartRequest;
        }

        public GetFullDepthChartRequest GetFullDepthChartRequest { get; }
    }

    public class GetFullDepthChartCommandHandler : IRequestHandler<GetFullDepthChartCommand, IEnumerable<Models.Player>>
    {
        private readonly ISportRepository _sportRepository;

        public GetFullDepthChartCommandHandler(ISportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }

        public Task<IEnumerable<Models.Player>> Handle(GetFullDepthChartCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_sportRepository.GetFullDepthChart(request.GetFullDepthChartRequest));
            }
            catch (Exception ex)
            {
                //TODO: Custom exception
                throw new Exception(ex.Message);
            }
        }
    }
}

