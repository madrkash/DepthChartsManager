using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationException(string leagueName)
			: base($"Validation errors: {leagueName}")
		{

		}
	}
}

