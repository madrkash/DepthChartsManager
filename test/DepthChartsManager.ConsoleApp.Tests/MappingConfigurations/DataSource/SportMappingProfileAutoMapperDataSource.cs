using System.Collections;
using DepthChartsManager.Common.Constants;

namespace DepthChartsManager.Console.Tests.MappingConfigurations.DataSource
{
    public class LeagueAutoMapperDataSource : IEnumerable<object[]>
	{ 
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new Core.Models.League(new Common.Request.CreateLeagueRequest
                {
                    Id = 1, Name = "NFL"
                })
            };
          
        }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TeamAutoMapperDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        { 
            yield return new object[]
            {
                new Core.Models.Team(1, 1, "NFL")
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class PlayerAutoMapperDataSource : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            
            yield return new object[]
            {
                new Core.Models.Player(1, 1, 1, "John Doe", NFLPositions.QB, null)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

   
}



