using System.Diagnostics.Contracts;

namespace DepthChartsManager.Core.Models
{
    public class Player
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public int TeamId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int? PositionDepth { get; set;}
    }
}

