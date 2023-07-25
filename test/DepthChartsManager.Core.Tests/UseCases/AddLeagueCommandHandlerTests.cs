using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Core.UseCases.League;
using Moq;
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

            var leagueToAdd = new League(createLeagueRequest.Id, createLeagueRequest.Name);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<League>(createLeagueRequest)).Returns(leagueToAdd);

            var mockLeagueRepository = new Mock<ISportRepository>();
            mockLeagueRepository.Setup(r => r.AddLeague(createLeagueRequest)).Returns(leagueToAdd);

            var commandHandler = new AddLeagueCommandHandler(mockMapper.Object, mockLeagueRepository.Object);
            var command = new AddLeagueCommand(createLeagueRequest);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(leagueToAdd.Id, result.Id);
            Assert.Equal(leagueToAdd.Name, result.Name);

            // Verify that the repository method was called with the correct parameter
            mockLeagueRepository.Verify(r => r.AddLeague(createLeagueRequest), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var createLeagueRequest = new CreateLeagueRequest
            {
                Id = 1,
                Name = "Test League"
            };

            var mockMapper = new Mock<IMapper>();
            var mockLeagueRepository = new Mock<ISportRepository>();
            mockLeagueRepository.Setup(r => r.AddLeague(createLeagueRequest)).Throws(new Exception("Repository error"));

            var commandHandler = new AddLeagueCommandHandler(mockMapper.Object, mockLeagueRepository.Object);
            var command = new AddLeagueCommand(createLeagueRequest);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(command, CancellationToken.None));
        }
    }
}

