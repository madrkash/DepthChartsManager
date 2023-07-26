using System;
using DepthChartsManager.Common.Request;

namespace DepthChartsManager.Common.Builders
{
	public class GetFullDepthChartRequestBuilder
	{
        private GetAllPlayersRequest _getFullDepthChartRequest = new GetAllPlayersRequest();

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

        public GetAllPlayersRequest Build() => _getFullDepthChartRequest;
    }
}

