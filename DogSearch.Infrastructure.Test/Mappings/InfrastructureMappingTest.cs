using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoMapper;
using DogSearch.Infrastructure.Mapping;

namespace DogSearch.Infrastructure.Test.Mappings;

public class InfrastructureMappingTest
{
    [Fact]
    public void Mappings_should_be_correct()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<InfrastructureMapping>());
        config.AssertConfigurationIsValid();
    }

}