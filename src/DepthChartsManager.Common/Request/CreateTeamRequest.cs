
namespace DepthChartsManager.Common.Request
{
    public record CreateTeamRequest
	{
        public int LeagueId { get; set; }
        public int Id { get; set; }
        public string TeamName { get; set; }
    }
}

