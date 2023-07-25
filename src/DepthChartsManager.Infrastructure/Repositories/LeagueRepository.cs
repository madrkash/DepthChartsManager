using DepthChartsManager.Common.Request;
using DepthChartsManager.Core.Contracts;
using DepthChartsManager.Core.Models;

namespace DepthChartsManager.Infrastructure.Repositories
{
	public class LeagueRepository : ILeagueRepository
	{
        private List<League> _leagues = new List<League>();

        public  League AddLeague(CreateLeagueRequest createLeagueRequest)
        {
            var league = new League(createLeagueRequest);
            _leagues.Add(league);
            return league;
        }

        public League GetLeague(int leagueId)
        {
            return _leagues.Find(league => league.Id == leagueId);
        }

        public IEnumerable<League> GetLeagues()
        {
            return _leagues;
        }
    }
}

