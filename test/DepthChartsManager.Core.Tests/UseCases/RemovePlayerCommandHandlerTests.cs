using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Core.UseCases.Player;
using Moq;
namespace DepthChartsManager.Core.Tests.UseCases
{
    public class RemovePlayerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldRemovePlayerFromDepthChart()
        {
            // Arrange
            var removePlayerRequest = new RemovePlayerRequest
            {
                Id = 1,
                Name = "John Doe",
                Position = "Forward",
                LeagueId = 1,
                TeamId = 1
            };

            var playerToRemove = new Player(1, 1, 1, "John Doe", "Forward", 0);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Player>(removePlayerRequest)).Returns(playerToRemove);

            var mockSportRepository = new Mock<ISportRepository>();
            mockSportRepository.Setup(r => r.RemovePlayerFromDepthChart(removePlayerRequest)).Returns(playerToRemove);

            var commandHandler = new RemovePlayerCommandHandler(mockMapper.Object, mockSportRepository.Object);
            var command = new RemovePlayerCommand(removePlayerRequest);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(playerToRemove.Id, result.Id);
            Assert.Equal(playerToRemove.Name, result.Name);
            Assert.Equal(playerToRemove.Position, result.Position);

            // Verify that the repository method was called with the correct parameter
            mockSportRepository.Verify(r => r.RemovePlayerFromDepthChart(removePlayerRequest), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var removePlayerRequest = new RemovePlayerRequest
            {
                Id = 1,
                Name = "John Doe",
                Position = "Forward",
                LeagueId = 1,
                TeamId = 1
            };

            var mockMapper = new Mock<IMapper>();
            var mockSportRepository = new Mock<ISportRepository>();
            mockSportRepository.Setup(r => r.RemovePlayerFromDepthChart(removePlayerRequest)).Throws(new Exception("Repository error"));

            var commandHandler = new RemovePlayerCommandHandler(mockMapper.Object, mockSportRepository.Object);
            var command = new RemovePlayerCommand(removePlayerRequest);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(command, CancellationToken.None));
        }
    }

}

