using System;
using DepthChartsManager.Common.Constants;
using DepthChartsManager.Common.Request;
using DepthChartsManager.ConsoleApp.Validators;
using FluentValidation.TestHelper;

namespace DepthChartsManager.Console.Tests.Validators
{
	public class GetPlayerBackupsRequestValidatorShould
    {
        private readonly GetPlayerBackupsRequestValidator _validator = new GetPlayerBackupsRequestValidator();
       

        [Fact]
        public void Have_Error_When_PlayerId_Is_Invalid()
        {
            var model = new GetPlayerBackupsRequest { TeamId = 2, LeagueId = 1, Name = "Test Player Name", Position = NFLPositions.LWR };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(getPlayerBackupsRequest => getPlayerBackupsRequest.PlayerId);
        }

        [Fact]
        public void Have_Error_When_LeagueId_Is_Invalid()
        {
            var model = new GetPlayerBackupsRequest { TeamId = 2, PlayerId = 1, Name = "Test Player Name", Position = NFLPositions.LWR };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(getPlayerBackupsRequest => getPlayerBackupsRequest.LeagueId);
        }

        [Fact]
        public void Have_Error_When_TeamId_Is_Invalid()
        {
            var model = new GetPlayerBackupsRequest { LeagueId = 2, PlayerId = 1, Name = "Test Player Name", Position = NFLPositions.LWR };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(getPlayerBackupsRequest => getPlayerBackupsRequest.TeamId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Name_Is_Invalid(string invalidName)
        {
            var model = new GetPlayerBackupsRequest { PlayerId = 1, Name = invalidName, LeagueId = 2, Position = NFLPositions.RB, TeamId = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(getPlayerBackupsRequest => getPlayerBackupsRequest.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Position_Is_Invalid(string invalidPosition)
        {
            var model = new GetPlayerBackupsRequest { PlayerId = 1, TeamId = 1, LeagueId = 2, Position = invalidPosition, Name = "John Doe" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(getPlayerBackupsRequest => getPlayerBackupsRequest.Position);
        }
    }
}

    

