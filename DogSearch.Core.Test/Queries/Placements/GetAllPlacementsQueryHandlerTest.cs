using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Queries.Placements;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Queries.Placements;

public class GetAllPlacementsQueryHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAll_from_repository_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        GetAllPlacementsQuery query,
        IEnumerable<Placement> placements,
        CancellationToken token,
        GetAllPlacementsQueryHandler sut)
    {
        //Arrange
        placementRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(placements);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        placementRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        results.Should().BeEquivalentTo(placements);
    }
}