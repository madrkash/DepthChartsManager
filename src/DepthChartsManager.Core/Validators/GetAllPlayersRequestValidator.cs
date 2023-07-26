using System;
using DepthChartsManager.Common.Request;
using FluentValidation;

namespace DepthChartsManager.ConsoleApp.Validators
{
	public class GetAllPlayersRequestValidator: AbstractValidator<GetAllPlayersRequest>
	{
		public GetAllPlayersRequestValidator()
		{
			RuleFor(x => x.LeagueId).NotEmpty();
            RuleFor(x => x.TeamId).NotEmpty();
        }
	}
}

