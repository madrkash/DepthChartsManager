using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Core.Contracts
{
    public interface ILeagueRepository
	{
        Models.League AddLeague(League league);
        IEnumerable<Models.League> GetLeagues();
        Models.League GetLeague(int leagueId);
    }
}

