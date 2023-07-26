using System;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Infrastructure.Repositories
{
	public class TeamRepository : ITeamRepository
	{
        private List<Team> _teams;
		public TeamRepository()
		{
            _teams = new List<Team>();
        }

        public Team AddTeam(Team team)
        {
            _teams.Add(team);
            return team;
        }

        public IEnumerable<Team> GetTeams(int leagueId)
        {
            return _teams;
        }

        public Team GetTeam(int id)
        {
            return _teams.Find(t => t.Id == id);
        }
    }
}

