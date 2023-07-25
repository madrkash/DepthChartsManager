using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class PlayerAlreadyExistsException : Exception
	{
		public PlayerAlreadyExistsException(string playerName, string position)
			: base($"Player {playerName} already exists at the position {position}")
		{

		}
	}
}

