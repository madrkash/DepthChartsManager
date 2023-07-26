using DepthChartsManager.Common.Request;
using FluentValidation;

namespace DepthChartsManager.ConsoleApp.Validators
{
    public class CreateLeagueRequestValidator: AbstractValidator<CreateLeagueRequest>
	{
		public CreateLeagueRequestValidator()
		{
			RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
        }
	}
}

