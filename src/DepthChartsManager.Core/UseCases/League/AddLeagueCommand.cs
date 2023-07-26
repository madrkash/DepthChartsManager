
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using MediatR;

namespace DepthChartsManager.Core.UseCases.League
{
    public class AddLeagueCommand : IRequest<Models.League>
    {
        public AddLeagueCommand(CreateLeagueRequest createLeagueRequest)
        {
            CreateLeagueRequest = createLeagueRequest;
        }

        public CreateLeagueRequest CreateLeagueRequest { get; }
    }

    public class AddLeagueCommandHandler : IRequestHandler<AddLeagueCommand, Models.League>
    {
        private readonly IMapper _mapper;
        private readonly ISportRepository _sportRepository;

        public AddLeagueCommandHandler(IMapper mapper, ISportRepository sportRepository)
        {
            _mapper = mapper;
            _sportRepository = sportRepository;
        }

        public Task<Models.League> Handle(AddLeagueCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_sportRepository.AddLeague(request.CreateLeagueRequest));
        }
    }
}

