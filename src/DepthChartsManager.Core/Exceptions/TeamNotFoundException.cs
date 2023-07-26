using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class TeamNotFoundException : Exception
	{
		public TeamNotFoundException(int leagueId, int teamId)
            : base($"{nameof(Models.Team)} with ID {teamId} in {nameof(Models.League)} with ID {leagueId} not found")
        {

		}
	}
}

