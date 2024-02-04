using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Queries.Dogs;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Queries.Dogs;

public class GetDogQueryHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_GetById_from_repository_when_handle(
        [Frozen] Mock<IDogRepository> dogRepositoryMock,
        GetDogQuery query,
        Dog dog,
        CancellationToken token,
        GetDogQueryHandler sut)
    {
        //Arrange
        dogRepositoryMock.Setup(x => x.GetById(It.IsAny<DogId>())).ReturnsAsync(dog);

        //Act
        var result = await sut.Handle(query, token);

        //Assert
        dogRepositoryMock.Verify(x => x.GetById(It.IsAny<DogId>()), Times.Once);
        result.Id.Should().Be(dog.Id);
        result.Name.Should().Be(dog.Name);
        result.Size.Should().Be(dog.Size);
        result.Breed.Should().Be(dog.Breed);
        result.OwnerId.Should().Be(dog.OwnerId);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetById_with_corect_values_when_handle(
        [Frozen] Mock<IDogRepository> dogRepositoryMock,
        GetDogQuery query,
        Dog dog,
        CancellationToken token,
        GetDogQueryHandler sut)
    {
        //Arrange
        DogId callbackDogId = null;
        dogRepositoryMock.Setup(x => x.GetById(It.IsAny<DogId>()))
            .Callback<DogId>((id) =>
            {
                callbackDogId = id;
            }).ReturnsAsync(dog);

        //Act
        await sut.Handle(query, token);

        //Assert
        callbackDogId.Should().Be(query.Id);
    }
}