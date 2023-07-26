using System;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Infrastructure.Repositories
{
	public class PlayerRepository : IPlayerRepository
	{
        private readonly List<Player> _players;

		public PlayerRepository()
		{
            _players = new List<Player>();
        }

        public Player AddPlayerToDepthChart(Player player)
        {
            return player;
        }

        public IEnumerable<Player> GetBackups(GetPlayerBackupsRequest getPlayerBackupsRequest)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> GetFullDepthChart(GetFullDepthChartRequest getFullDepthChartRequest)
        {
            throw new NotImplementedException();
        }

        public Player RemovePlayerFromDepthChart(RemovePlayerRequest removePlayerFromDepthChartRequest)
        {
            throw new NotImplementedException();
        }
    }
}

