
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.Models;
using Moq;
using Xunit;
namespace DepthChartsManager.Core.Tests.Models
{


    public class TeamTests
    {
        [Fact]
        public void AddPlayer_WhenThePlayerListIsEmpty_ShouldAddPlayer()
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

            var team = new Team(1, 1, "Test Team");

            // Act
            var result = team.AddPlayer(createPlayerRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Single(team.Players);
            Assert.Equal("John Doe", team.Players[0].Name);
            Assert.Equal("Forward", team.Players[0].Position);
        }

        [Fact]
        public void AddPlayer_WhenPlayerListHasData_ShouldAddPlayerAtTheDesiredPosition_And_UpdateTheRelevantPlayerPositions()
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

            var createPlayerRequest1 = new CreatePlayerRequest
            {
                Id = 2,
                LeagueId = 1,
                TeamId = 1,
                Name = "Smith Doe",
                Position = "Forward",
                PositionDepth = null
            };

            var createPlayerRequest2 = new CreatePlayerRequest
            {
                Id = 3,
                LeagueId = 1,
                TeamId = 1,
                Name = "Philip Doe",
                Position = "Forward",
                PositionDepth = null
            };

            var team = new Team(1, 1, "Test Team");
            var john = team.AddPlayer(createPlayerRequest);
            var smith = team.AddPlayer(createPlayerRequest1);
            var philip = team.AddPlayer(createPlayerRequest2);

            // Act
            var createPlayerRequest3 = new CreatePlayerRequest
            {
                Id = 4,
                LeagueId = 1,
                TeamId = 1,
                Name = "Prashanth Bhat",
                Position = "Forward",
                PositionDepth = 1
            };

            var prashanth = team.AddPlayer(createPlayerRequest3);

            // Assert
            Assert.NotNull(prashanth);
            Assert.Equal(4, team.Players.Count);
            Assert.Equal(1, prashanth.PositionDepth);
            Assert.Equal("Smith Doe", team.Players[2].Name);
            Assert.Equal(2, team.Players[2].PositionDepth);
            Assert.Equal("Philip Doe", team.Players[3].Name);
            Assert.Equal(3, team.Players[3].PositionDepth);
        }

        [Fact]
        public void AddPlayer_WhenPlayerExists_ShouldThrowPlayerAlreadyExistsException()
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

            var team = new Team(1, 1, "Test Team");
            team.AddPlayer(createPlayerRequest);

            // Act & Assert
            Assert.Throws<PlayerAlreadyExistsException>(() => team.AddPlayer(createPlayerRequest));
        }

        [Fact]
        public void RemovePlayer_WhenPlayerExists_ShouldRemovePlayer()
        {
            // Arrange
            var player = new Player(1, 1, 1, "John Doe", "Forward", 0);
            var team = new Team(1, 1, "Test Team");
            team.Players.Add(player);

            // Act
            var result = team.RemovePlayer(1, "John Doe", "Forward");

            // Assert
            Assert.NotNull(result);
            Assert.Empty(team.Players);
            Assert.Equal("John Doe", result.Name);
            Assert.Equal("Forward", result.Position);
        }

        [Fact]
        public void RemovePlayer_WhenPlayerExists_ShouldRemovePlayerAndUpdatePositions()
        {
            // Arrange
            var team = new Team(1, 1, "Test Team");
            team.Players.Add(new Player(1, 1, 1, "John Doe", "Forward", 0));
            team.Players.Add(new Player(2, 1, 1, "Steve Smith", "Forward", 1));
            team.Players.Add(new Player(3, 1, 1, "Ricky Ponting", "Forward", 2));


            // Act
            var result = team.RemovePlayer(1, "John Doe", "Forward");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, team.Players.Count);
            Assert.Equal("Steve Smith", team.Players[0].Name);
            Assert.Equal(0, team.Players[0].PositionDepth);
            Assert.Equal("Ricky Ponting", team.Players[1].Name);
            Assert.Equal(1, team.Players[1].PositionDepth);
        }

       
        [Fact]
        public void RemovePlayer_WhenPlayerDoesNotExist_ShouldReturnNull()
        {
            // Arrange
            var team = new Team(1, 1, "Test Team");

            // Act
            var result = team.RemovePlayer(1, "John Doe", "Forward");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetBackups_ShouldReturnCorrectBackups()
        {
            // Arrange
            var team = new Team(1, 1, "Test Team");
            team.Players.AddRange(new List<Player>
            {
            new Player(1, 1, 1, "John Doe", "Forward", 0),
            new Player(2, 1, 1, "Jane Smith", "Forward", 1),
            new Player(3, 1, 1, "Michael Johnson", "Forward", 2),
            new Player(4, 1, 1, "Mark Davis", "Midfielder", 0),
            new Player(5, 1, 1, "Sarah Adams", "Midfielder", 1),
        });

            // Act
            var backups = team.GetBackups(1, "John Doe", "Forward");

            // Assert
            Assert.Equal(2, backups.Count());
            Assert.Contains(backups, p => p.Name == "Jane Smith");
            Assert.Contains(backups, p => p.Name == "Michael Johnson");
        }

        [Fact]
        public void GetBackups_WhenPlayerDoesNotExist_ShouldReturnEmptyList()
        {
            // Arrange
            var team = new Team(1, 1, "Test Team");
            team.Players.AddRange(new List<Player>
            {
            new Player(1, 1, 1, "John Doe", "Forward", 0),
            new Player(2, 1, 1, "Jane Smith", "Forward", 1),
            new Player(3, 1, 1, "Michael Johnson", "Forward", 2),
            new Player(4, 1, 1, "Mark Davis", "Midfielder", 0),
            new Player(5, 1, 1, "Sarah Adams", "Midfielder", 1),
        });

            // Act
            var backups = team.GetBackups(6, "Non-Existent Player", "Goalkeeper");

            // Assert
            Assert.Empty(backups);
        }

        [Fact]
        public void GetFullDepthChart_ShouldReturnCorrectPlayers()
        {
            // Arrange
            var team = new Team(1, 1, "Test Team");
            team.Players.AddRange(new List<Player>
            {
            new Player(1, 1, 1, "John Doe", "Forward", 0),
            new Player(2, 1, 1, "Jane Smith", "Forward", 1),
            new Player(3, 1, 1, "Michael Johnson", "Forward", 2),
            new Player(4, 1, 1, "Mark Davis", "Midfielder", 0),
            new Player(5, 1, 1, "Sarah Adams", "Midfielder", 1),
        });

            // Act
            var fullDepthChart = team.GetFullDepthChart(1, 1);

            // Assert
            Assert.Equal(5, fullDepthChart.Count());
            Assert.Contains(fullDepthChart, p => p.Name == "John Doe");
            Assert.Contains(fullDepthChart, p => p.Name == "Jane Smith");
            Assert.Contains(fullDepthChart, p => p.Name == "Michael Johnson");
            Assert.Contains(fullDepthChart, p => p.Name == "Mark Davis");
            Assert.Contains(fullDepthChart, p => p.Name == "Sarah Adams");
        }
    }

}

