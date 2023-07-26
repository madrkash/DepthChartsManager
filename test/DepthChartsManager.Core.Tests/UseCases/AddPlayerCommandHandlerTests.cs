using DepthChartsManager.Common.Builders;
using DepthChartsManager.Common.Constants;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Exceptions;
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
            var createPlayerRequest = new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(1)
                .WithTeamId(1)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .Build();

            var players = new List<Player> {
              new Player
            {
                Id = 2,
                LeagueId = 1,
                TeamId = 1,
                Name = "Jane Smith",
                Position = NFLPositions.LWR,
                PositionDepth = 0
            }, new Player
            {
                Id = 3,
                LeagueId = 1,
                TeamId = 1,
                Name = "Michael Johnson",
                Position = NFLPositions.RB,
                PositionDepth = 0
            }};
             
            var league = new League
            {
                Id = 1,
                Name = "TestLeague"
            };
            var team = new Team
            {
                Id = 1,
                LeagueId = 1,
                Name = "TestTeam"
            };
             

            var leagueRepositoryMock = new Mock<ILeagueRepository>();
            leagueRepositoryMock.Setup(r => r.GetLeague(1))
                .Returns(league);

            var teamRepositoryMock = new Mock<ITeamRepository>();
            teamRepositoryMock.Setup(r => r.GetTeams(1))
                .Returns(new List<Team> { team });

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(r => r.GetAllPlayers(It.IsAny<GetAllPlayersRequest>()))
                .Returns(players);
            playerRepositoryMock.Setup(r => r.AddPlayerToDepthChart(It.IsAny<Player>()))
                .Returns<Player>(p => p); // Return the player as it is for verification purposes
          
            var commandHandler = new AddPlayerCommandHandler(leagueRepositoryMock.Object, teamRepositoryMock.Object, playerRepositoryMock.Object);

            // Act
            var result = await commandHandler.Handle(new AddPlayerCommand(createPlayerRequest), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createPlayerRequest.Id, result.Id);
            Assert.Equal(createPlayerRequest.Name, result.Name);
            Assert.Equal(createPlayerRequest.Position, result.Position);
            Assert.Equal(1, result.PositionDepth);

            // Verify that the repository methods were called with the correct parameters
            leagueRepositoryMock.Verify(r => r.GetLeague(createPlayerRequest.LeagueId), Times.Once);
            teamRepositoryMock.Verify(r => r.GetTeams(createPlayerRequest.LeagueId), Times.Once);
            playerRepositoryMock.Verify(r => r.GetAllPlayers(It.IsAny<GetAllPlayersRequest>()), Times.Once);
            playerRepositoryMock.Verify(r => r.AddPlayerToDepthChart(It.IsAny<Player>()), Times.Once);
        }

        [Fact]
        public async Task Handle_PlayerAlreadyExists_ShouldThrowPlayerAlreadyExistsException()
        {
            // Arrange
            var createPlayerRequest = new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(1)
                .WithTeamId(1)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .Build();


            var league = new League
            {
                Id = 1,
                Name = "TestLeague"
            };
            var team = new Team
            {
                Id = 1,
                LeagueId = 1,
                Name = "TestTeam"
            };

            var players = new List<Player> {
               new Player
                {
                Id = 1,
                LeagueId = 1,
                TeamId = 1,
                Name = "Tom Brady",
                Position = NFLPositions.LWR,
                PositionDepth = 1
                },
              new Player
                {
                Id = 2,
                LeagueId = 1,
                TeamId = 1,
                Name = "Jane Smith",
                Position = NFLPositions.LWR,
                PositionDepth = 0
                },
              new Player
                {
                Id = 3,
                LeagueId = 1,
                TeamId = 1,
                Name = "Michael Johnson",
                Position = NFLPositions.RB,
                PositionDepth = 0
                }
            };

            var leagueRepositoryMock = new Mock<ILeagueRepository>();
            leagueRepositoryMock.Setup(r => r.GetLeague(createPlayerRequest.LeagueId))
                .Returns(league);

            var teamRepositoryMock = new Mock<ITeamRepository>();
            teamRepositoryMock.Setup(r => r.GetTeams(createPlayerRequest.LeagueId))
                .Returns(new List<Team> { team });

            var playerRepositoryMock = new Mock<IPlayerRepository>();
            playerRepositoryMock.Setup(r => r.GetAllPlayers(It.IsAny<GetAllPlayersRequest>()))
                .Returns(players);

            var commandHandler = new AddPlayerCommandHandler(leagueRepositoryMock.Object, teamRepositoryMock.Object, playerRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<PlayerAlreadyExistsException>(() => commandHandler.Handle(new AddPlayerCommand(createPlayerRequest), CancellationToken.None));
        }

        [Fact]
        public async Task Handle_LeagueNotFound_ShouldThrowLeagueNotFoundException()
        {
            // Arrange
            var createPlayerRequest = new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(1)
                .WithTeamId(1)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .Build();


            var league = new League
            {
                Id = 1,
                Name = "TestLeague"
            };
            var team = new Team
            {
                Id = 1,
                LeagueId = 1,
                Name = "TestTeam"
            };

            var leagueRepositoryMock = new Mock<ILeagueRepository>();
            leagueRepositoryMock.Setup(r => r.GetLeague(createPlayerRequest.LeagueId)).Returns<League>(null);

            var teamRepositoryMock = new Mock<ITeamRepository>();

            var playerRepositoryMock = new Mock<IPlayerRepository>();

            var commandHandler = new AddPlayerCommandHandler(leagueRepositoryMock.Object, teamRepositoryMock.Object, playerRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<LeagueNotFoundException>(() => commandHandler.Handle(new AddPlayerCommand(createPlayerRequest), CancellationToken.None));

        }

        [Fact]
        public async Task Handle_TeamNotFound_ShouldThrowTeamNotFoundException()
        {
            // Arrange
            var createPlayerRequest = new CreatePlayerRequestBuilder()
                .WithDefaultValues()
                .WithLeagueId(1)
                .WithTeamId(1)
                .WithName("Tom Brady")
                .WithPosition(NFLPositions.LWR)
                .Build();


            var league = new League
            {
                Id = 1,
                Name = "TestLeague"
            };
            var team = new Team
            {
                Id = 1,
                LeagueId = 1,
                Name = "TestTeam"
            };

            var leagueRepositoryMock = new Mock<ILeagueRepository>();
            leagueRepositoryMock.Setup(r => r.GetLeague(createPlayerRequest.LeagueId)).Returns(league);

            var teamRepositoryMock = new Mock<ITeamRepository>();
            teamRepositoryMock.Setup(r => r.GetTeams(createPlayerRequest.LeagueId))
                .Returns(new List<Team>{});

            var playerRepositoryMock = new Mock<IPlayerRepository>();

            var commandHandler = new AddPlayerCommandHandler(leagueRepositoryMock.Object, teamRepositoryMock.Object, playerRepositoryMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<TeamNotFoundException>(() => commandHandler.Handle(new AddPlayerCommand(createPlayerRequest), CancellationToken.None));

        }
    }


}

