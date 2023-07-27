using System;
namespace DepthChartsManager.Core.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationException(string validationErrors)
			: base($"Validation errors: {validationErrors}")
		{

		}
	}
}

