using System;
using DepthChartsManager.Common.Request;

namespace DepthChartsManager.Common.Builders
{
	public class GetAllPlayersRequestBuilder
	{
        private GetAllPlayersRequest _getFullDepthChartRequest = new GetAllPlayersRequest();

        public GetAllPlayersRequestBuilder WithLeagueId(int leagueId)
        {
            _getFullDepthChartRequest.LeagueId = leagueId;
            return this;
        }

        public GetAllPlayersRequestBuilder WithTeamId(int teamId)
        {
            _getFullDepthChartRequest.TeamId = teamId;
            return this;
        }

        public GetAllPlayersRequest Build() => _getFullDepthChartRequest;
    }
}

