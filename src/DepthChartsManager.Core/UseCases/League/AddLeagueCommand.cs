
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Exceptions;
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
        private readonly ILeagueRepository _leagueRepository;

        public AddLeagueCommandHandler(IMapper mapper, ILeagueRepository leagueRepository)
        {
            _mapper = mapper;
            _leagueRepository = leagueRepository;
        }

        public Task<Models.League> Handle(AddLeagueCommand request, CancellationToken cancellationToken)
        {
            var leagues = _leagueRepository.GetLeagues().ToList();

            if(DoesLeagueExist(request, leagues))
            {
                throw new LeagueAlreadyExistsException(request.CreateLeagueRequest.Name);
            }

            return Task.FromResult(_leagueRepository.AddLeague(new Models.League { Id = request.CreateLeagueRequest.Id, Name = request.CreateLeagueRequest.Name }));
        }

        private static bool DoesLeagueExist(AddLeagueCommand request, List<Models.League> leagues) => leagues.Any() && leagues.Any(league => league.Name == request.CreateLeagueRequest.Name);
    }
}

