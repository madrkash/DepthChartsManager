using System;
using DepthChartsManager.Common.Request;

namespace DepthChartsManager.Common.Builders
{
	public class CreateTeamRequestBuilder
	{
        private CreateTeamRequest _createTeamRequest = new CreateTeamRequest();

        public CreateTeamRequestBuilder WithLeagueId(int leagueId)
        {
            _createTeamRequest.LeagueId = leagueId;
            return this;
        } 

        public CreateTeamRequestBuilder WithTeamName(string teamName)
        {
            _createTeamRequest.TeamName = teamName;
            return this;
        }

        public CreateTeamRequestBuilder WithDefaultValues()
        {
            _createTeamRequest.Id = ++EntityCounter.TeamCount;
            return this;
        }

        public CreateTeamRequest Build() => _createTeamRequest;
    }
}

