using Xunit;
using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Commands;
using Moq;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;

namespace DogSearch.Core.Test.Commands;

public class CreateDogCommandHandlerTest
{

    [Theory]
    [AutoMoqData]
    public async void Should_call_create_from_repository_when_handle(
        [Frozen] Mock<IDogRepository> dogRepositoryMock,
        CreateDogCommand command, 
        CancellationToken token, 
        CreateDogCommandHandler sut)
    {
        //Arrange
        dogRepositoryMock.Setup(x => x.Create(It.IsAny<Dog>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        dogRepositoryMock.Verify(x => x.Create(It.IsAny<Dog>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_create_with_corect_values_when_handle(
        [Frozen] Mock<IDogRepository> dogRepositoryMock,
        CreateDogCommand command,
        CancellationToken token,
        CreateDogCommandHandler sut)
    {
        //Arrange
        Dog callbackDog = null;
        dogRepositoryMock.Setup(x => x.Create(It.IsAny<Dog>()))
            .Callback<Dog>((d) =>
            {
                callbackDog = d;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackDog.Name.Should().Be(command.Name);
        callbackDog.Breed.Should().Be(command.Breed);
        callbackDog.Size.Should().Be(command.Size);
        callbackDog.OwnerId.Should().Be(command.OwnerId);
    }
}