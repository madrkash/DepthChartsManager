using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class PlayerAlreadyExistsException : Exception
	{
		public PlayerAlreadyExistsException(string playerName, string position)
			: base($"{nameof(Models.Player)} with name {playerName} already exists at the position {position}")
		{

		}
	}
}

