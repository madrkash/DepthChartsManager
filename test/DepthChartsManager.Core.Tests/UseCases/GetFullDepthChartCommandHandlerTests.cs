using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Core.UseCases.Player;
using Moq;


namespace DepthChartsManager.Core.Tests.UseCases
{

    public class GetFullDepthChartCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnFullDepthChart()
        {
            // Arrange
            var getFullDepthChartRequest = new GetAllPlayersRequest
            {
                LeagueId = 1,
                TeamId = 1
            };

            var players = new List<Player>
        {
            new Player { Id = 1, LeagueId = 1, TeamId = 1, Name = "Player 1", Position = "QB", PositionDepth = 1 },
            new Player { Id = 2, LeagueId = 1, TeamId = 1, Name = "Player 2", Position = "QB", PositionDepth = 2 },
            new Player { Id = 3, LeagueId = 1, TeamId = 1, Name = "Player 3", Position = "RB", PositionDepth = 1 },
        };

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(r => r.GetAllPlayers(getFullDepthChartRequest))
                .Returns(players);

            var commandHandler = new GetFullDepthChartCommandHandler(playerRepositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(new GetFullDepthChartCommand(getFullDepthChartRequest), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());

            // Verify that the repository method was called with the correct parameters
            playerRepositoryMock.Verify(r => r.GetAllPlayers(getFullDepthChartRequest), Times.Once);
        }

        [Fact]
        public async Task Handle_EmptyDepthChart_ShouldReturnEmptyList()
        {
            // Arrange
            var getFullDepthChartRequest = new GetAllPlayersRequest
            {
                LeagueId = 2,
                TeamId = 2
            };

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(r => r.GetAllPlayers(getFullDepthChartRequest))
                .Returns(new List<Player>());

            var commandHandler = new GetFullDepthChartCommandHandler(playerRepositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(new GetFullDepthChartCommand(getFullDepthChartRequest), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            // Verify that the repository method was called with the correct parameters
            playerRepositoryMock.Verify(r => r.GetAllPlayers(getFullDepthChartRequest), Times.Once);
        }

    }


}

