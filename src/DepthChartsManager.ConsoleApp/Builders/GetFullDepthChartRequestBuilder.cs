using System;
using DepthChartsManager.Common.Request;

namespace DepthChartsManager.ConsoleApp.Builders
{
	public class GetFullDepthChartRequestBuilder
	{
        private GetFullDepthChartRequest _getFullDepthChartRequest = new GetFullDepthChartRequest();

        public GetFullDepthChartRequestBuilder WithLeagueId(int leagueId)
        {
            _getFullDepthChartRequest.LeagueId = leagueId;
            return this;
        }

        public GetFullDepthChartRequestBuilder WithTeamId(int teamId)
        {
            _getFullDepthChartRequest.TeamId = teamId;
            return this;
        }

        public GetFullDepthChartRequest Build() => _getFullDepthChartRequest;
    }
}

