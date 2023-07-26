using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.UseCases.League;
using Moq;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Core.Tests.UseCases
{


    public class AddLeagueCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldAddLeague()
        {
            // Arrange
            var createLeagueRequest = new CreateLeagueRequest
            {
                Id = 1,
                Name = "Test League"
            };

            var leagues = new List<League>
                {
                    new League { Id = 2, Name = "Other League" }
                };

            var leagueRepositoryMock = new Mock<ILeagueRepository>();
            leagueRepositoryMock.Setup(r => r.GetLeagues())
                .Returns(leagues);
            leagueRepositoryMock.Setup(r => r.AddLeague(It.IsAny<League>()))
                .Returns<League>(league => league); // Return the league as it is for verification purposes

            var mapperMock = new Mock<IMapper>();

            var commandHandler = new AddLeagueCommandHandler(mapperMock.Object, leagueRepositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(new AddLeagueCommand(createLeagueRequest), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createLeagueRequest.Id, result.Id);
            Assert.Equal(createLeagueRequest.Name, result.Name);

            // Verify that the repository methods were called with the correct parameters
            leagueRepositoryMock.Verify(r => r.GetLeagues(), Times.Once);
            leagueRepositoryMock.Verify(r => r.AddLeague(It.IsAny<League>()), Times.Once);
        }

        [Fact]
        public async Task Handle_LeagueAlreadyExists_ShouldThrowLeagueAlreadyExistsException()
        {
            // Arrange
            var createLeagueRequest = new CreateLeagueRequest
            {
                Id = 1,
                Name = "Test League"
            };

            var leagues = new List<League>
        {
            new League { Id = 1, Name = "Test League" },
            new League { Id = 2, Name = "Other League" }
        };

            var leagueRepositoryMock = new Mock<ILeagueRepository>();
            leagueRepositoryMock.Setup(r => r.GetLeagues())
                .Returns(leagues);

            var mapperMock = new Mock<IMapper>();

            var commandHandler = new AddLeagueCommandHandler(mapperMock.Object, leagueRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<LeagueAlreadyExistsException>(() => commandHandler.Handle(new AddLeagueCommand(createLeagueRequest), CancellationToken.None));
        }
    }

}

