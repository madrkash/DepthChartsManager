using System;
using DepthChartsManager.Common.Constants;
using DepthChartsManager.Common.Request;
using DepthChartsManager.ConsoleApp.Validators;
using FluentValidation.TestHelper;

namespace DepthChartsManager.Console.Tests.Validators
{
	public class CreateTeamRequestValidatorShould
	{
        private readonly CreateTeamRequestValidator _validator = new CreateTeamRequestValidator();
      
        [Fact]
        public void Have_Error_When_Id_Is_Invalid()
        {
            var model = new CreateTeamRequest { TeamName = "Test Team Name", LeagueId = 2};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createTeamRequest => createTeamRequest.Id);
        }

        [Fact]
        public void Have_Error_When_LeagueId_Is_Invalid()
        {
            var model = new CreateTeamRequest { Id = 2, TeamName = "Test Team Name"};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createTeamRequest => createTeamRequest.LeagueId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_TeamName_Is_Invalid(string invalidName)
        {
            var model = new CreateTeamRequest {Id = 1, TeamName = invalidName, LeagueId = 2 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createTeamRequest => createTeamRequest.TeamName);
        }
    }
}

    

