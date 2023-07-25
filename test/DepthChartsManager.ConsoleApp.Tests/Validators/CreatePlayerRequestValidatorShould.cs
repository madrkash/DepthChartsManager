using DepthChartsManager.Common.Constants;
using DepthChartsManager.Common.Request;
using DepthChartsManager.ConsoleApp.Validators;
using FluentValidation.TestHelper;

namespace DepthChartsManager.Console.Tests.Validators
{
    public class CreatePlayerRequestValidatorShould
	{
        private readonly CreatePlayerRequestValidator _validator = new CreatePlayerRequestValidator();

        [Fact]
        public void Have_Error_When_Id_Is_Invalid()
        {
            var model = new CreatePlayerRequest { Name = "John Smith", LeagueId = 2, TeamId = 2, Position = NFLPositions.LWR, PositionDepth = 0};
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createPlayerRequest => createPlayerRequest.Id);
        }

        [Fact]
        public void Have_Error_When_LeagueId_Is_Invalid()
        {
            var model = new CreatePlayerRequest { Id = 2, Name = "John Smith", TeamId = 2, Position = NFLPositions.LWR, PositionDepth = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createPlayerRequest => createPlayerRequest.LeagueId);
        }

        [Fact]
        public void Have_Error_When_TeamId_Is_Invalid()
        {
            var model = new CreatePlayerRequest { Id = 2, Name = "John Smith", LeagueId = 2, Position = NFLPositions.LWR, PositionDepth = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createPlayerRequest => createPlayerRequest.TeamId);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Name_Is_Invalid(string invalidName)
        {
            var model = new CreatePlayerRequest {Id = 1, Name = invalidName, LeagueId = 2, TeamId = 2, Position = NFLPositions.LWR, PositionDepth = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createPlayerRequest => createPlayerRequest.Name);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Position_Is_Invalid(string invalidPosition)
        {
            var model = new CreatePlayerRequest { Id = 1, Name = "Jane Goodall", LeagueId = 2, TeamId = 2, Position = invalidPosition, PositionDepth = 0 };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(createPlayerRequest => createPlayerRequest.Position);
        }

    }
}

    

