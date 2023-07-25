using System;
namespace DepthChartsManager.Common.Request
{
	public class CreatePlayerRequest
	{
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int? PositionDepth { get; set; }
    }
}

