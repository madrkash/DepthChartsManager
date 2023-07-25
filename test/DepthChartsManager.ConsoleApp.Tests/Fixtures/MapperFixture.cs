using System;
using AutoMapper;
using DepthChartsManager.ConsoleApp.MappingConfigurations;

namespace DepthChartsManager.Console.Tests.Fixtures
{
	public class MapperFixture
	{
        public IMapper Mapper { get; }

        public MapperFixture()
        {
            var config = new MapperConfiguration(opts =>
            {
                opts.AddProfile<SportMappingProfile>();
            });

            Mapper = config.CreateMapper();
        }
    }
}

