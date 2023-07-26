using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Core.UseCases.League;
using DepthChartsManager.Core.UseCases.Player;
using Moq;

namespace DepthChartsManager.Core.Tests.UseCases
{
    public class GetPlayerBackupsCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnBackupPlayers()
        {
            // Arrange
            var getPlayerBackupsRequest = new GetPlayerBackupsRequest
            {
                LeagueId = 1,
                TeamId = 1,
                PlayerId = 2,
                Position = "QB"
            };

            var players = new List<Player>
        {
            new Player { Id = 1, LeagueId = 1, TeamId = 1, Position = "QB", PositionDepth = 0},
            new Player { Id = 2, LeagueId = 1, TeamId = 1, Position = "QB", PositionDepth = 1 },
            new Player { Id = 3, LeagueId = 1, TeamId = 1, Position = "QB", PositionDepth = 2 },
        };

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(r => r.GetAllPlayers(It.IsAny<GetAllPlayersRequest>()))
                .Returns<GetAllPlayersRequest>(request => players.Where(p => p.LeagueId == request.LeagueId && p.TeamId == request.TeamId).ToList());

            var commandHandler = new GetPlayerBackupsCommandHandler(playerRepositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(new GetPlayerBackupsCommand(getPlayerBackupsRequest), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal(3, result.First().Id);
            Assert.Equal(2, result.First().PositionDepth);

            // Verify that the repository method was called with the correct parameters
            playerRepositoryMock.Verify(r => r.GetAllPlayers(It.IsAny<GetAllPlayersRequest>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NoBackupPlayers_ShouldReturnEmptyList()
        {
            // Arrange
            var getPlayerBackupsRequest = new GetPlayerBackupsRequest
            {
                LeagueId = 1,
                TeamId = 1,
                PlayerId = 1,
                Position = "QB"
            };

            var players = new List<Player>
        {
            new Player { Id = 1, LeagueId = 1, TeamId = 1, Position = "QB", PositionDepth = 1 },
            new Player { Id = 3, LeagueId = 1, TeamId = 1, Position = "RB", PositionDepth = 1 },
        };

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(r => r.GetAllPlayers(It.IsAny<GetAllPlayersRequest>()))
                .Returns<GetAllPlayersRequest>(request => players.Where(p => p.LeagueId == request.LeagueId && p.TeamId == request.TeamId).ToList());

            var commandHandler = new GetPlayerBackupsCommandHandler(playerRepositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(new GetPlayerBackupsCommand(getPlayerBackupsRequest), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);

            // Verify that the repository method was called with the correct parameters
            playerRepositoryMock.Verify(r => r.GetAllPlayers(It.IsAny<GetAllPlayersRequest>()), Times.Once);
        }

        [Fact]
        public async Task Handle_NoPlayersAvailable_ShouldThrowPlayersNotFoundException()
        {
            // Arrange
            var getPlayerBackupsRequest = new GetPlayerBackupsRequest
            {
                LeagueId = 1,
                TeamId = 1,
                PlayerId = 1,
                Position = "QB"
            };

            var players = new List<Player> { };

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(r => r.GetAllPlayers(It.IsAny<GetAllPlayersRequest>()))
                .Returns(players);

            var commandHandler = new GetPlayerBackupsCommandHandler(playerRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<PlayersNotFoundException>(() => commandHandler.Handle(new GetPlayerBackupsCommand(getPlayerBackupsRequest), CancellationToken.None));
        }
    }


}

