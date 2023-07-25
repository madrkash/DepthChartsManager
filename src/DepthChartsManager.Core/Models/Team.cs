using DepthChartsManager.Core.Exceptions;
using DepthChartsManager.Common.Request;

namespace DepthChartsManager.Core.Models
{
    public class Team
    {
        private List<Player> _players = new List<Player>();

        public int Id { get; }
        public int LeagueId { get; }
        public string Name { get; }
        public List<Player> Players
        {
            get
            {
                return _players;
            }
        }

        public Team(int id, int leagueId, string name)
        {
            Id = id;
            LeagueId = leagueId;
            Name = name;
        }

        public Player AddPlayer(CreatePlayerRequest createPlayerRequest)
        {
            if (_players.Exists(player =>
            string.Equals(player.Name, createPlayerRequest.Name, StringComparison.OrdinalIgnoreCase) &&
            string.Equals(player.Position, createPlayerRequest.Position, StringComparison.OrdinalIgnoreCase)))
            {
                throw new PlayerAlreadyExistsException(createPlayerRequest.Name, createPlayerRequest.Position);
            }

            int index = CalculatePositionDepth(createPlayerRequest);

            var player = new Player(createPlayerRequest.Id, createPlayerRequest.LeagueId, createPlayerRequest.TeamId, createPlayerRequest.Name, createPlayerRequest.Position, index);
            _players.Add(player);
            return player;
        }

        private int CalculatePositionDepth(CreatePlayerRequest createPlayerRequest)
        {
            int index = 0;

            if (createPlayerRequest.PositionDepth == null)
            {
                if (_players.Any())
                {
                    var existingPlayers = _players.Where(x => x.Position == createPlayerRequest.Position).ToList();
                    if (existingPlayers.Any())
                    {
                        index = (int)(existingPlayers.Last().PositionDepth + 1);
                    }
                }
            }

            return index;
        }

        public Player RemovePlayer(int playerId, string name, string position)
        {
            var player = _players.Find(p => p.Id == playerId && string.Equals(name, p.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(position, p.Position, StringComparison.OrdinalIgnoreCase));

            var backupPlayers = _players.OrderBy(nextPlayer => nextPlayer.PositionDepth > player.PositionDepth).ToList();

            backupPlayers.ForEach(player => player.PositionDepth = player.PositionDepth - 1);

            _players.Remove(player);
            return player;
        }
        public IEnumerable<Player> GetBackups(int playerId, string name, string position)
        {
            var playerPositionDepth = _players.FindIndex(p => p.Id == playerId && string.Equals(position, p.Position, StringComparison.OrdinalIgnoreCase));
            var players = _players.Where(player => player.Position == position).Skip(playerPositionDepth + 1);
            return players;
        }
        public IEnumerable<Player> GetFullDepthChart(int leagueId, int teamId)
        {
            var players = _players.Where(player => player.LeagueId == leagueId && player.TeamId == teamId);
            return players;
        }
    }
}

