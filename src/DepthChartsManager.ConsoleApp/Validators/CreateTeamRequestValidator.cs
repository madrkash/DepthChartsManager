using System;
using DepthChartsManager.Common.Request;
using FluentValidation;

namespace DepthChartsManager.ConsoleApp.Validators
{
	public class CreateTeamRequestValidator: AbstractValidator<CreateTeamRequest>
	{
		public CreateTeamRequestValidator()
		{
			RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.LeagueId).NotEmpty();
            RuleFor(x => x.TeamName).NotEmpty();
        }
	}
}

