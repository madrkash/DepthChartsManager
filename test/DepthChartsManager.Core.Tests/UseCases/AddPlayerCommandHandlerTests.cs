using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Core.UseCases.Player;
using Moq;

namespace DepthChartsManager.Core.Tests.UseCases
{
    public class AddPlayerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldAddPlayerToDepthChart()
        {
            // Arrange
            var createPlayerRequest = new CreatePlayerRequest
            {
                Id = 1,
                LeagueId = 1,
                TeamId = 1,
                Name = "John Doe",
                Position = "Forward",
                PositionDepth = null
            };

            var playerToAdd = new Player(1, 1, 1, "John Doe", "Forward", 0);


            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(m => m.Map<Player>(createPlayerRequest)).Returns(playerToAdd);

            var mockSportRepository = new Mock<ISportRepository>();
            mockSportRepository.Setup(r => r.AddPlayerToDepthChart(createPlayerRequest)).Returns(playerToAdd);

            var commandHandler = new AddPlayerCommandHandler(mockSportRepository.Object);
            var command = new AddPlayerCommand(createPlayerRequest);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(playerToAdd.Id, result.Id);
            Assert.Equal(playerToAdd.Name, result.Name);
            Assert.Equal(playerToAdd.Position, result.Position);

            // Verify that the repository method was called with the correct parameter
            mockSportRepository.Verify(r => r.AddPlayerToDepthChart(createPlayerRequest), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var createPlayerRequest = new CreatePlayerRequest
            {
                Id = 1,
                LeagueId = 1,
                TeamId = 1,
                Name = "John Doe",
                Position = "Forward",
                PositionDepth = null
            };

            var mockMapper = new Mock<IMapper>();
            var mockSportRepository = new Mock<ISportRepository>();
            mockSportRepository.Setup(r => r.AddPlayerToDepthChart(createPlayerRequest)).Throws(new Exception("Repository error"));

            var commandHandler = new AddPlayerCommandHandler(mockSportRepository.Object);
            var command = new AddPlayerCommand(createPlayerRequest);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(command, CancellationToken.None));
        }
    }

}

