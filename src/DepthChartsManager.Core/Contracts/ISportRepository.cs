using System;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Core.Contracts
{
	public interface ISportRepository
	{
        Models.League AddLeague(CreateLeagueRequest league);
        Models.League GetLeague(int leagueId);
        Team AddTeam(CreateTeamRequest team);
        Player AddPlayerToDepthChart(CreatePlayerRequest player);
        Player RemovePlayerFromDepthChart(RemovePlayerRequest player);
        IEnumerable<Player> GetBackups(GetPlayerBackupsRequest getPlayerBackupsDto);
        IEnumerable<Player> GetFullDepthChart(GetAllPlayersRequest getFullDepthChartDto);
    }
}

