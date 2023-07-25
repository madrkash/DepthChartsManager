using DepthChartsManager.Common.Request;

namespace DepthChartsManager.ConsoleApp.Builders
{
	public class CreateLeagueRequestBuilder
	{
		private CreateLeagueRequest _createLeagueRequest = new CreateLeagueRequest();

		public CreateLeagueRequestBuilder WithName(string name)
		{
			_createLeagueRequest.Name = name;
			return this;
		}

		public CreateLeagueRequestBuilder WithDefaultValues()
		{
			_createLeagueRequest.Id = ++EntityCounter.LeagueCount;
			return this;
		}

		public CreateLeagueRequest Build() => _createLeagueRequest;
	}
}

