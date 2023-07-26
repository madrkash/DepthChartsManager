using System.Collections.Generic;
using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Core.Contracts
{
    public interface ILeagueRepository
	{
        Models.League AddLeague(League league);
        Models.League GetLeague(int leagueId);
        IEnumerable<Models.League> GetLeagues();
    }
}

