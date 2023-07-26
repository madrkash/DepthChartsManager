using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class LeagueNotFoundException : Exception
	{
		public LeagueNotFoundException(int leagueId)
			: base($"{nameof(Models.League)} with ID {leagueId} not found")
		{

		}
	}
}

