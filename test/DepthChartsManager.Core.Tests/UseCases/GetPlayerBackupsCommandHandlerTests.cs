using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;
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
                PlayerId = 1,
                Position = "Forward",
                LeagueId = 1,
                Name = "Jane Smith",
                TeamId = 1
            };

        var backupPlayers = new List<Player>
        {
            new Player(1, 1, 1, "Jane Smith", "Forward",0),
            new Player(2, 1, 1, "Michael Johnson", "Forward",1)
        };

            var mockMapper = new Mock<IMapper>();
            var mockSportRepository = new Mock<ISportRepository>();
            mockSportRepository.Setup(r => r.GetBackups(getPlayerBackupsRequest)).Returns(backupPlayers);

            var commandHandler = new GetPlayerBackupsCommandHandler(mockSportRepository.Object);
            var command = new GetPlayerBackupsCommand(getPlayerBackupsRequest);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());

            var resultPlayer1 = result.ElementAt(0);
            Assert.Equal(backupPlayers[0].Id, resultPlayer1.Id);
            Assert.Equal(backupPlayers[0].Name, resultPlayer1.Name);
            Assert.Equal(backupPlayers[0].Position, resultPlayer1.Position);

            var resultPlayer2 = result.ElementAt(1);
            Assert.Equal(backupPlayers[1].Id, resultPlayer2.Id);
            Assert.Equal(backupPlayers[1].Name, resultPlayer2.Name);
            Assert.Equal(backupPlayers[1].Position, resultPlayer2.Position);

            // Verify that the repository method was called with the correct parameter
            mockSportRepository.Verify(r => r.GetBackups(getPlayerBackupsRequest), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var getPlayerBackupsRequest = new GetPlayerBackupsRequest
            {
                PlayerId = 1,
                Position = "Forward"
            };

            var mockMapper = new Mock<IMapper>();
            var mockSportRepository = new Mock<ISportRepository>();
            mockSportRepository.Setup(r => r.GetBackups(getPlayerBackupsRequest)).Throws(new Exception("Repository error"));

            var commandHandler = new GetPlayerBackupsCommandHandler(mockSportRepository.Object);
            var command = new GetPlayerBackupsCommand(getPlayerBackupsRequest);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(command, CancellationToken.None));
        }
    }

}

