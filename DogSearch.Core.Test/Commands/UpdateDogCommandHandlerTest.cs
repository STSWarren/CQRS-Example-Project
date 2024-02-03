using AutoFixture.Xunit2;
using DogSearch.Core.Commands.Dogs;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands;

public class UpdateDogCommandHandlerTest
{

    [Theory]
    [AutoMoqData]
    public async void Should_call_Update_from_repository_when_handle(
        [Frozen] Mock<IDogRepository> dogRepositoryMock,
        UpdateDogCommand command,
        CancellationToken token,
        UpdateDogCommandHandler sut)
    {
        //Arrange
        dogRepositoryMock.Setup(x => x.Update(It.IsAny<DogId>(),It.IsAny<Dog>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        dogRepositoryMock.Verify(x => x.Update(It.IsAny<DogId>(), It.IsAny<Dog>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_Update_with_corect_values_when_handle(
        [Frozen] Mock<IDogRepository> dogRepositoryMock,
        UpdateDogCommand command,
        CancellationToken token,
        UpdateDogCommandHandler sut)
    {
        //Arrange
        Dog callbackDog = null;
        DogId callbackDogId = null;
        dogRepositoryMock.Setup(x => x.Update(It.IsAny<DogId>(), It.IsAny<Dog>()))
            .Callback<DogId,Dog>((id, d) =>
            {
                callbackDogId = id;
                callbackDog = d;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackDogId.Should().Be(command.Id);
        callbackDog.Name.Should().Be(command.Name);
        callbackDog.Breed.Should().Be(command.Breed);
        callbackDog.Size.Should().Be(command.Size);
        callbackDog.OwnerId.Should().Be(command.OwnerId);
    }
}