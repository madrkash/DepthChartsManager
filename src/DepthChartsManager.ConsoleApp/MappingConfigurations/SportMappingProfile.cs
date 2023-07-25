using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Common.Response;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.ConsoleApp.MappingConfigurations
{
	public class SportMappingProfile : Profile
	{
		public SportMappingProfile()
		{
            CreateMap<League, LeagueResponse>();
			CreateMap<Team, TeamResponse>();
            CreateMap<Player, PlayerResponse>();
			CreateMap<Player, GetPlayerBackupsRequest>()
				.ForMember(dest => dest.PlayerId, opt => opt.MapFrom(source => source.Id));
			CreateMap<Player, RemovePlayerRequest>();
        }
	}
}

