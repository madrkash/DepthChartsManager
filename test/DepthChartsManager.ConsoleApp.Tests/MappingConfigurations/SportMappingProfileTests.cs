using System;
using DepthChartsManager.Common.Response;
using DepthChartsManager.Console.Tests.Fixtures;
using DepthChartsManager.Console.Tests.MappingConfigurations.DataSource;
using DepthChartsManager.Core.Models;
using FluentAssertions;

namespace DepthChartsManager.Console.Tests.MappingConfigurations
{
	public class SportMappingProfileTests
	{
        [Theory]
        [ClassData(typeof(LeagueAutoMapperDataSource))]
        public void Map_League_To_LeagueResponse_Should_Work(League league)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<LeagueResponse>(league);

            result.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(TeamAutoMapperDataSource))]
        public void Map_Team_To_TeamResponse_Should_Work(Team team)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<TeamResponse>(team);

            result.Should().NotBeNull();
        }

        [Theory]
        [ClassData(typeof(PlayerAutoMapperDataSource))]
        public void Map_Player_To_PlayerResponse_Should_Work(Player player)
        {
            var mapper = new MapperFixture().Mapper;
            var result = mapper.Map<PlayerResponse>(player);

            result.Should().NotBeNull();
        }
    }
}

