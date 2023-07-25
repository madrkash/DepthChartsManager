
using FluentValidation.TestHelper;
using DepthChartsManager.ConsoleApp.Validators;
using System.ComponentModel.DataAnnotations;
using DepthChartsManager.Common.Request;

namespace DepthChartsManager.Console.Tests.Validators
{
	public class CreateLeagueRequestValidatorShould
	{
        private readonly CreateLeagueRequestValidator _validator = new CreateLeagueRequestValidator();

        [Fact]
        public void Have_Error_When_Id_Is_Invalid()
        {
            var model = new CreateLeagueRequest { Name = "NHL" };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.Id);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void Have_Error_When_Name_Is_Invalid(string invalidName)
        {
            var model = new CreateLeagueRequest { Id = 1, Name = invalidName };
            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(productUpdateRequest => productUpdateRequest.Name);
        }
    }
}

