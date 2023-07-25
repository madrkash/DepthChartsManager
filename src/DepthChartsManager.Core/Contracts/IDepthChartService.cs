using System;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Core.Contracts
{
	public interface IDepthChartService
	{
        //Component testing
        //Change management
        Task<Models.League> AddLeague(CreateLeagueRequest createLeagueRequest);
        Task<Team> AddTeam(CreateTeamRequest createLeagueRequest);
        Task<Player> AddPlayerToDepthChart(CreatePlayerRequest createPlayerRequest);
        Task<Player> RemovePlayerFromDepthChart(RemovePlayerRequest removePlayerFromDepthChartRequest);
        Task<IEnumerable<Player>> GetPlayerBackups(GetPlayerBackupsRequest getPlayerBackupsRequest);
        Task<IEnumerable<Player>> GetFullDepthChart(GetFullDepthChartRequest getFullDepthChartRequest);
    }
}

