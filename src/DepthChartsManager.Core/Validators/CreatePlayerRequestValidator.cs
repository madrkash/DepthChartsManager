using DepthChartsManager.Common.Request;
using FluentValidation;

namespace DepthChartsManager.ConsoleApp.Validators
{
    public class CreatePlayerRequestValidator : AbstractValidator<CreatePlayerRequest>
	{
		public CreatePlayerRequestValidator()
		{
			RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.LeagueId).NotEmpty();
            RuleFor(x => x.TeamId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Position).NotEmpty();
        }
	}
}

