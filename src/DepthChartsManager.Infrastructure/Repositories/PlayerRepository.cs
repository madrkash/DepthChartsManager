using System;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Infrastructure.Repositories
{
	public class PlayerRepository : IPlayerRepository
	{
        private List<Player> _players;

		public PlayerRepository()
		{
            _players = new List<Player>();
        }
         
        public Player AddPlayerToDepthChart(Player player)
        {
            _players.Add(player);
            return player;
        }

        /// <summary>
        /// Update selected player positions by joining with the original collection
        /// </summary>
        /// <param name="playersToBeUpdated"></param>
        public void UpdatePlayerPositions(IEnumerable<Player> playersToBeUpdated)
        {
            var teamPlayers = _players.Where(player => player.LeagueId == playersToBeUpdated.First().LeagueId && player.TeamId == playersToBeUpdated.First().TeamId).ToList();

            _ = teamPlayers.Join(playersToBeUpdated, teamPlayer => teamPlayer.Id, updatedPlayer => updatedPlayer.Id, (teamPlayer, updatedPlayer) =>
            {
                teamPlayer.PositionDepth = updatedPlayer.PositionDepth;
                return teamPlayer;
            });
        }
        
        public IEnumerable<Player> GetAllPlayers(GetAllPlayersRequest getAllPlayersRequest)
        {
            return _players.Where(player => player.LeagueId == getAllPlayersRequest.LeagueId && player.TeamId == getAllPlayersRequest.TeamId);
        }

        public Player RemovePlayerFromDepthChart(Player player)
        {
            _players.Remove(player);
            return player;
        }
    }
}

