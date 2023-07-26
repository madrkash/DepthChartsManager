using System;
using DepthChartsManager.Common.Constants;
using DepthChartsManager.Common.Request;
using DepthChartsManager.ConsoleApp.Validators;
using FluentValidation.TestHelper;

namespace DepthChartsManager.Console.Tests.Validators
{
	public class GetFullDepthChartRequestValidatorShould
    {
        private readonly GetAllPlayersRequestValidator _validator = new GetAllPlayersRequestValidator();
      
        [Fact]
        public void Have_Error_When_LeagueId_Is_Invalid()
        {
            var model = new GetAllPlayersRequest { TeamId = 2};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createTeamRequest => createTeamRequest.LeagueId);
        }

        [Fact]
        public void Have_Error_When_TeamId_Is_Invalid()
        {
            var model = new GetAllPlayersRequest { LeagueId = 2};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createTeamRequest => createTeamRequest.TeamId);
        }
    }
}

    

