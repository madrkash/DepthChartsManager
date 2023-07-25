using System;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using MediatR;

namespace DepthChartsManager.Core.UseCases.Team
{
    public class AddTeamCommand : IRequest<Models.Team>
    {
        public AddTeamCommand(CreateTeamRequest createTeamRequest)
        {
            CreateTeamRequest = createTeamRequest;
        }

        public CreateTeamRequest CreateTeamRequest { get; }
    }

    public class AddTeamCommandHandler : IRequestHandler<AddTeamCommand, Models.Team>
    {
        private readonly ISportRepository _sportRepository;

        public AddTeamCommandHandler(ISportRepository sportRepository)
        {  
            _sportRepository = sportRepository;
        }

        public Task<Models.Team> Handle(AddTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {
                return Task.FromResult(_sportRepository.AddTeam(request.CreateTeamRequest));
            }
            catch (Exception ex)
            {
                //TODO: Create custom / common exception
                throw new Exception(ex.Message);
            }
        }
    }
}


