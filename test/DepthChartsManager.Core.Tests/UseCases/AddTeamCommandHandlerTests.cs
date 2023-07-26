using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Core.UseCases.Team;
using Moq;
namespace DepthChartsManager.Core.Tests.UseCases
{

    public class AddTeamCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldAddTeam()
        {
            // Arrange
            var createTeamRequest = new CreateTeamRequest
            {
                Id = 1,
                LeagueId = 1,
                TeamName = "Test Team"
            };

            var league = new League { Id = 1 };
            var teams = new List<Team>
        {
            new Team { Id = 2, LeagueId = 1, Name = "Other Team" }
        };

            var leagueRepositoryMock = new Mock<ILeagueRepository>();
            leagueRepositoryMock.Setup(r => r.GetLeague(createTeamRequest.LeagueId))
                .Returns(league);

            var teamRepositoryMock = new Mock<ITeamRepository>();
            teamRepositoryMock.Setup(r => r.GetTeams(createTeamRequest.LeagueId))
                .Returns(teams);
            teamRepositoryMock.Setup(r => r.AddTeam(It.IsAny<Team>()))
                .Returns<Team>(team => team); // Return the team as it is for verification purposes

            var commandHandler = new AddTeamCommandHandler(leagueRepositoryMock.Object, teamRepositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(new AddTeamCommand(createTeamRequest), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createTeamRequest.Id, result.Id);
            Assert.Equal(createTeamRequest.LeagueId, result.LeagueId);
            Assert.Equal(createTeamRequest.TeamName, result.Name);

            // Verify that the repository methods were called with the correct parameters
            leagueRepositoryMock.Verify(r => r.GetLeague(createTeamRequest.LeagueId), Times.Once);
            teamRepositoryMock.Verify(r => r.GetTeams(createTeamRequest.LeagueId), Times.Once);
            teamRepositoryMock.Verify(r => r.AddTeam(It.IsAny<Team>()), Times.Once);
        }

        [Fact]
        public async Task Handle_TeamAlreadyExists_ShouldThrowTeamAlreadyExistsException()
        {
            // Arrange
            var createTeamRequest = new CreateTeamRequest
            {
                Id = 1,
                LeagueId = 1,
                TeamName = "Test Team"
            };

            var league = new League { Id = 1 };
            var teams = new List<Team>
        {
            new Team { Id = 1, LeagueId = 1, Name = "Test Team" },
            new Team { Id = 2, LeagueId = 1, Name = "Other Team" }
        };

            var leagueRepositoryMock = new Mock<ILeagueRepository>();
            leagueRepositoryMock.Setup(r => r.GetLeague(createTeamRequest.LeagueId))
                .Returns(league);

            var teamRepositoryMock = new Mock<ITeamRepository>();
            teamRepositoryMock.Setup(r => r.GetTeams(createTeamRequest.LeagueId))
                .Returns(teams);

            var commandHandler = new AddTeamCommandHandler(leagueRepositoryMock.Object, teamRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<TeamAlreadyExistsException>(() => commandHandler.Handle(new AddTeamCommand(createTeamRequest), CancellationToken.None));
        }

        [Fact]
        public async Task Handle_LeagueNotFound_ShouldThrowLeagueNotFoundException()
        {
            // Arrange
            var createTeamRequest = new CreateTeamRequest
            {
                Id = 1,
                LeagueId = 1,
                TeamName = "Test Team"
            };

            var league = new League { Id = 1 };
            var teams = new List<Team>
        {
            new Team { Id = 1, LeagueId = 1, Name = "Test Team" },
            new Team { Id = 2, LeagueId = 1, Name = "Other Team" }
        };

            var leagueRepositoryMock = new Mock<ILeagueRepository>();
            leagueRepositoryMock.Setup(r => r.GetLeague(createTeamRequest.LeagueId))
                .Returns<League>(null);

            var teamRepositoryMock = new Mock<ITeamRepository>();

            var commandHandler = new AddTeamCommandHandler(leagueRepositoryMock.Object, teamRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<LeagueNotFoundException>(() => commandHandler.Handle(new AddTeamCommand(createTeamRequest), CancellationToken.None));
        }
    }

}



