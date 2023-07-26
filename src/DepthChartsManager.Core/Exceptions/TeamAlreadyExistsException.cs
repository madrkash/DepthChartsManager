using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class TeamAlreadyExistsException : Exception
	{
		public TeamAlreadyExistsException(string teamName)
			: base($"Team {teamName} already exists")
		{

		}
	}
}

