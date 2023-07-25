using System;
using System.Diagnostics.Contracts;
using System.Xml.Linq;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Infrastructure.Repositories
{
	public class SportRepository : ISportRepository
	{
        private readonly List<League> _leagues;

        public SportRepository()
		{
            _leagues = new List<League>();
        }

        public League GetLeague(int leagueId)
        {
            return _leagues.Find(s => s.Id == leagueId);
        }

        public League AddLeague(CreateLeagueRequest createLeagueRequest)
        {    
            if(_leagues.Exists(league => string.Equals(league.Name, createLeagueRequest.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new LeagueAlreadyExistsException(createLeagueRequest.Name);
            }

            var league = new League(createLeagueRequest.Id, createLeagueRequest.Name);
            _leagues.Add(league);
            return league;
        }

        public Player AddPlayerToDepthChart(CreatePlayerRequest createPlayerRequest)
        {
            return GetLeague(createPlayerRequest.LeagueId)?
                .GetTeam(createPlayerRequest.TeamId)?
                .AddPlayer(createPlayerRequest);
        }

        public Team AddTeam(CreateTeamRequest createTeamRequest)
        {
            return GetLeague(createTeamRequest.LeagueId)?
                .AddTeam(new Team(createTeamRequest.Id, createTeamRequest.LeagueId, createTeamRequest.TeamName));
        }

        public IEnumerable<Player> GetBackups(GetPlayerBackupsRequest getPlayerBackupsRequest)
        {
            return GetLeague(getPlayerBackupsRequest.LeagueId)?
                .GetTeam(getPlayerBackupsRequest.TeamId)?
                .GetBackups(getPlayerBackupsRequest.PlayerId, getPlayerBackupsRequest.Name, getPlayerBackupsRequest.Position);
        }

        public IEnumerable<Player> GetFullDepthChart(GetFullDepthChartRequest getFullDepthChartRequest)
        {
            return GetLeague(getFullDepthChartRequest.LeagueId)?
                .GetTeam(getFullDepthChartRequest.TeamId)?
                .GetFullDepthChart(getFullDepthChartRequest.LeagueId, getFullDepthChartRequest.TeamId);
        }
  

        public Player RemovePlayerFromDepthChart(RemovePlayerRequest removePlayerRequest)
        {
            return GetLeague(removePlayerRequest.LeagueId)?
                .GetTeam(removePlayerRequest.TeamId)?
                .RemovePlayer(removePlayerRequest.Id, removePlayerRequest.Name, removePlayerRequest.Position);
        }
    }
}


