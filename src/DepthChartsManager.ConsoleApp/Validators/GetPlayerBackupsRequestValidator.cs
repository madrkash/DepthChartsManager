using DepthChartsManager.Common.Request;
using FluentValidation;

namespace DepthChartsManager.ConsoleApp.Validators
{
    public class GetPlayerBackupsRequestValidator : AbstractValidator<GetPlayerBackupsRequest>
	{
		public GetPlayerBackupsRequestValidator()
		{
			RuleFor(x => x.LeagueId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
			RuleFor(x => x.PlayerId).NotEmpty();
			RuleFor(x => x.Position).NotEmpty();
            RuleFor(x => x.TeamId).NotEmpty();
        }
	}
}

