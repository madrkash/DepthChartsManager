using System;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Core.Contracts
{
	public interface IPlayerRepository
	{
        Player AddPlayerToDepthChart(Player player);
        Player RemovePlayerFromDepthChart(Player player);
        IEnumerable<Player> GetAllPlayers(GetAllPlayersRequest getFullDepthChartRequest);
        void UpdatePlayerPositions(IEnumerable<Player> players);
    }
}

