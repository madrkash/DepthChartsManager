using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class PlayerNotFoundException : Exception
	{
		public PlayerNotFoundException(int teamId, string position)
            : base($"No {nameof(Models.Player)} found for {nameof(Models.Team)} with ID {teamId} at position {position}")
        {

		}
	}
}

