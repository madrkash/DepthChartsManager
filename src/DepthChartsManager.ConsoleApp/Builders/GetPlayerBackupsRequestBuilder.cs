using System;
using DepthChartsManager.Common.Request;

namespace DepthChartsManager.ConsoleApp.Builders
{
	public class GetPlayerBackupsRequestBuilder
	{  
        private GetPlayerBackupsRequest _getPlayerBackupsRequest = new GetPlayerBackupsRequest();

        public GetPlayerBackupsRequestBuilder WithLeagueId(int leagueId)
        {
            _getPlayerBackupsRequest.LeagueId = leagueId;
            return this;
        }

        public GetPlayerBackupsRequestBuilder WithTeamId(int teamId)
        {
            _getPlayerBackupsRequest.TeamId = teamId;
            return this;
        }

        public GetPlayerBackupsRequestBuilder WithPlayerId(int playerId)
        {
            _getPlayerBackupsRequest.PlayerId = playerId;
            return this;
        }

        public GetPlayerBackupsRequestBuilder WithName(string name)
        {
            _getPlayerBackupsRequest.Name = name;
            return this;
        }

        public GetPlayerBackupsRequestBuilder WithPosition(string position)
        {
            _getPlayerBackupsRequest.Position = position;
            return this;
        }

        public GetPlayerBackupsRequest Build() => _getPlayerBackupsRequest;
    }
}

