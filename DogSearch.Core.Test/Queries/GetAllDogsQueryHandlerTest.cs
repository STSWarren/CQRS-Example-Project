using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Queries.Dogs;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Queries;

public class GetAllDogsQueryHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAll_from_repository_when_handle(
        [Frozen] Mock<IDogRepository> dogRepositoryMock,
        GetAllDogsQuery query,
        IEnumerable<Dog> dogs,
        CancellationToken token,
        GetAllDogsQueryHandler sut)
    {
        //Arrange
        dogRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(dogs);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        dogRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        results.Should().BeEquivalentTo(dogs);
    }
}