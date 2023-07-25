using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;
using DepthChartsManager.Core.UseCases.Player;
using Moq;
using Xunit;

namespace DepthChartsManager.Core.Tests.UseCases
{
    public class GetFullDepthChartCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidRequest_ShouldReturnFullDepthChart()
        {
            // Arrange
            var getFullDepthChartRequest = new GetFullDepthChartRequest
            {
                LeagueId = 1,
                TeamId = 1
            };

            var fullDepthChart = new List<Player>
            {
                new Player(1, 1, 1, "John Doe", "Forward",0),
                new Player(2, 1, 1, "Jane Smith", "Forward",0),
                new Player(3, 1, 1, "Michael Johnson", "Midfielder",0),
            };

            var mockSportRepository = new Mock<ISportRepository>();
            mockSportRepository.Setup(r => r.GetFullDepthChart(getFullDepthChartRequest)).Returns(fullDepthChart);

            var commandHandler = new GetFullDepthChartCommandHandler(mockSportRepository.Object);
            var command = new GetFullDepthChartCommand(getFullDepthChartRequest);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());

            var resultPlayer1 = result.ElementAt(0);
            Assert.Equal(fullDepthChart[0].Id, resultPlayer1.Id);
            Assert.Equal(fullDepthChart[0].Name, resultPlayer1.Name);
            Assert.Equal(fullDepthChart[0].Position, resultPlayer1.Position);

            var resultPlayer2 = result.ElementAt(1);
            Assert.Equal(fullDepthChart[1].Id, resultPlayer2.Id);
            Assert.Equal(fullDepthChart[1].Name, resultPlayer2.Name);
            Assert.Equal(fullDepthChart[1].Position, resultPlayer2.Position);

            var resultPlayer3 = result.ElementAt(2);
            Assert.Equal(fullDepthChart[2].Id, resultPlayer3.Id);
            Assert.Equal(fullDepthChart[2].Name, resultPlayer3.Name);
            Assert.Equal(fullDepthChart[2].Position, resultPlayer3.Position);

            // Verify that the repository method was called with the correct parameter
            mockSportRepository.Verify(r => r.GetFullDepthChart(getFullDepthChartRequest), Times.Once);
        }

        [Fact]
        public async Task Handle_RepositoryThrowsException_ShouldThrowException()
        {
            // Arrange
            var getFullDepthChartRequest = new GetFullDepthChartRequest
            {
                LeagueId = 1,
                TeamId = 1
            };

            var mockMapper = new Mock<IMapper>();
            var mockSportRepository = new Mock<ISportRepository>();
            mockSportRepository.Setup(r => r.GetFullDepthChart(getFullDepthChartRequest)).Throws(new Exception("Repository error"));

            var commandHandler = new GetFullDepthChartCommandHandler(mockSportRepository.Object);
            var command = new GetFullDepthChartCommand(getFullDepthChartRequest);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => commandHandler.Handle(command, CancellationToken.None));
        }
    }

}

