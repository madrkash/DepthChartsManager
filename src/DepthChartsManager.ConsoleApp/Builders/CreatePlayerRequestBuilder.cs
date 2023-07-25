using System;
using DepthChartsManager.Common.Request;

namespace DepthChartsManager.ConsoleApp.Builders
{
	public class CreatePlayerRequestBuilder
	{
        private CreatePlayerRequest _createPlayerRequest = new CreatePlayerRequest();

        public CreatePlayerRequestBuilder WithLeagueId(int leagueId)
        {
            _createPlayerRequest.LeagueId = leagueId;
            return this;
        }

        public CreatePlayerRequestBuilder WithTeamId(int teamId)
        {
            _createPlayerRequest.TeamId = teamId;
            return this;
        }

        public CreatePlayerRequestBuilder WithName(string name)
        {
            _createPlayerRequest.Name = name;
            return this;
        }

        public CreatePlayerRequestBuilder WithPosition(string position)
        {
            _createPlayerRequest.Position = position;
            return this;
        }

        public CreatePlayerRequestBuilder WithPositionDepth(int positionDepth)
        {
            _createPlayerRequest.PositionDepth = positionDepth;
            return this;
        }

        public CreatePlayerRequestBuilder WithDefaultValues()
        {
            _createPlayerRequest.Id = ++EntityCounter.PlayerCount;
            return this;
        }

        public CreatePlayerRequest Build() => _createPlayerRequest;
    }
}

