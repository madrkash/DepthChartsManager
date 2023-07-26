using System;
using DepthChartsManager.Common.Request;

namespace DepthChartsManager.Common.Builders
{
	public class RemovePlayerRequestBuilder
	{
        private RemovePlayerRequest _removePlayerRequest = new RemovePlayerRequest();

        public RemovePlayerRequestBuilder WithLeagueId(int leagueId)
        {
            _removePlayerRequest.LeagueId = leagueId;
            return this;
        }

        public RemovePlayerRequestBuilder WithTeamId(int teamId)
        {
            _removePlayerRequest.TeamId = teamId;
            return this;
        }

        public RemovePlayerRequestBuilder WithId(int id)
        {
            _removePlayerRequest.Id = id;
            return this;
        }

        public RemovePlayerRequestBuilder WithName(string name)
        {
            _removePlayerRequest.Name = name;
            return this;
        }

        public RemovePlayerRequestBuilder WithPosition(string position)
        {
            _removePlayerRequest.Position = position;
            return this;
        }

        public RemovePlayerRequest Build() => _removePlayerRequest;
    }
}

