using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Core.UseCases.Team;
using Moq;
using Xunit;
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

            var teamToAdd = new Team(1,1,"Test Team");

            var mockTeamRepository = new Mock<ISportRepository>();
            mockTeamRepository.Setup(r => r.AddTeam(createTeamRequest)).Returns(teamToAdd);

            var commandHandler = new AddTeamCommandHandler(mockTeamRepository.Object);
            var command = new AddTeamCommand(createTeamRequest);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(teamToAdd.Id, result.Id);
            Assert.Equal(teamToAdd.LeagueId, result.LeagueId);
            Assert.Equal(teamToAdd.Name, result.Name);

            // Verify that the repository method was called with the correct parameter
            mockTeamRepository.Verify(r => r.AddTeam(createTeamRequest), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var createTeamRequest = new CreateTeamRequest
            {
                Id = 1,
                LeagueId = 1,
                TeamName = "Test Team"
            };

            var mockTeamRepository = new Mock<ISportRepository>();
            mockTeamRepository.Setup(r => r.AddTeam(createTeamRequest)).Throws(new Exception("Repository error"));

            var commandHandler = new AddTeamCommandHandler( mockTeamRepository.Object);
            var command = new AddTeamCommand(createTeamRequest);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(command, CancellationToken.None));
        }
    }

}

