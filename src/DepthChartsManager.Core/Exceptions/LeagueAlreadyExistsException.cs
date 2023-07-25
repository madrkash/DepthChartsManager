using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class LeagueAlreadyExistsException : Exception
	{
		public LeagueAlreadyExistsException(string leagueName)
			: base($"League {leagueName} already exists")
		{

		}
	}
}

