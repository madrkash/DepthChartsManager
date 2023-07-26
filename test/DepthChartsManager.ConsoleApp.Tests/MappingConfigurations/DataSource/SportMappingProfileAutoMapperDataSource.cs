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
                new Core.Models.League
                {
                    Id = 1,
                    Name = "NFL"
                }
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
                new Core.Models.Team{Id = 1, LeagueId = 1, Name = "NFL" }
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
                new Core.Models.Player{ Id = 1, LeagueId = 1, TeamId = 1, Name = "John Doe", Position = NFLPositions.QB, PositionDepth = null }
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

   
}



