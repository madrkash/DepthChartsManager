using System;
using DepthChartsManager.Common.Constants;
using DepthChartsManager.Common.Request;
using DepthChartsManager.ConsoleApp.Validators;
using FluentValidation.TestHelper;

namespace DepthChartsManager.Console.Tests.Validators
{
	public class RemovePlayerRequestValidatorShould
    {
        private readonly RemovePlayerRequestValidator _validator = new RemovePlayerRequestValidator();

        [Fact]
        public void Have_Error_When_Id_Is_Invalid()
        {
            var model = new RemovePlayerRequest { TeamId = 2, LeagueId = 1, Name = "John Smith", Position = NFLPositions.QB };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(removePlayerRequest => removePlayerRequest.Id);
        }

        [Fact]
        public void Have_Error_When_LeagueId_Is_Invalid()
        {
            var model = new RemovePlayerRequest { TeamId = 2, Id = 1, Name = "John Smith", Position = NFLPositions.RB };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(removePlayerRequest => removePlayerRequest.LeagueId);
        }

        [Fact]
        public void Have_Error_When_TeamId_Is_Invalid()
        {
            var model = new RemovePlayerRequest { LeagueId = 2, Id = 1, Name = "John Smith", Position = NFLPositions.RB };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(removePlayerRequest => removePlayerRequest.TeamId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Name_Is_Invalid(string invalidName)
        {
            var model = new RemovePlayerRequest { Id = 1, Name = invalidName, LeagueId = 2, Position = NFLPositions.RB, TeamId = 1 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(removePlayerRequest => removePlayerRequest.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Position_Is_Invalid(string invalidPosition)
        {
            var model = new RemovePlayerRequest { Id = 1, TeamId = 1, LeagueId = 2, Position = invalidPosition, Name = "John Doe" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(removePlayerRequest => removePlayerRequest.Position);
        }
    }
}

    

