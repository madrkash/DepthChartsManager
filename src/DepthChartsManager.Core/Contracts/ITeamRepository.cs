using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Core.Contracts
{
    public interface ITeamRepository
	{
        Team AddTeam(Team team);
        IEnumerable<Team> GetTeams(int leagueId);
    }
}

