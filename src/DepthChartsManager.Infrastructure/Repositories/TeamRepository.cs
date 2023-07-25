using System;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Infrastructure.Repositories
{
	public class TeamRepository : ITeamRepository
	{
		public TeamRepository()
		{

		}

        public Team AddTeam(Team team)
        {
            //Persist this wherever required
            return team;
        }

        public IEnumerable<Team> GetTeams(int leagueId)
        {
            throw new NotImplementedException();
        }
    }
}

