using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class PlayersNotFoundException : Exception
	{
		public PlayersNotFoundException(int leagueId, int teamId)
            : base($"No {nameof(Models.Player)} found for {nameof(Models.Team)} with ID {teamId} in {nameof(Models.League)} with ID {leagueId}")
        {

		}
	}
}

