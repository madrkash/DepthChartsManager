using System;
using DepthChartsManager.Common.Request;
using FluentValidation;

namespace DepthChartsManager.ConsoleApp.Validators
{
	public class GetFullDepthChartRequestValidator: AbstractValidator<GetFullDepthChartRequest>
	{
		public GetFullDepthChartRequestValidator()
		{
			RuleFor(x => x.LeagueId).NotEmpty();
            RuleFor(x => x.TeamId).NotEmpty();
        }
	}
}

