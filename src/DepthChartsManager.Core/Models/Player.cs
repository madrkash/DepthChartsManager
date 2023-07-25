using System.Diagnostics.Contracts;

namespace DepthChartsManager.Core.Models
{
    public class Player
    {
        public Player(int id, int leagueId, int teamId, string name, string position, int? positionDepth)
        { 
            Id = id;
            LeagueId = leagueId;
            TeamId = teamId;
            Name = name;
            Position = position;
            PositionDepth = positionDepth;
        }
        public int Id { get; }
        public int LeagueId { get; }
        public int TeamId { get; }
        public string Name { get; }
        public string Position { get; }
        public int? PositionDepth { get; set;}
    }
}

