    using DepthChartsManager.Common.Request;

namespace DepthChartsManager.Core.Models
{
    public class League
    {
        private List<Team> _teams = new List<Team>();

        public List<Team> Teams
        {
            get
            {
                return _teams;
            }
        }
        public int Id { get; set; }
        public string Name { get; set;}

        public League(int id, string name)
        {
            Id =  id;
            Name =  name;
        }

        //public League(CreateLeagueRequest createLeagueRequest)
        //{
        //    this.createLeagueRequest = createLeagueRequest;
        //}

        public Team GetTeam(int id)
        {
            return _teams.Find(t => t.Id == id);
        }

        public Team AddTeam(Team team)
        {
            _teams.Add(team);
            return team;
        }


    }
}

