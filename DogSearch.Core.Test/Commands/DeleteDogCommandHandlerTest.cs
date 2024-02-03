using AutoFixture.Xunit2;
using DogSearch.Core.Commands.Dogs;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands;

public class DeleteDogCommandHandlerTest
{

    [Theory]
    [AutoMoqData]
    public async void Should_call_delete_from_repository_when_handle(
        [Frozen] Mock<IDogRepository> dogRepositoryMock,
        DeleteDogCommand command,
        CancellationToken token,
        DeleteDogCommandHandler sut)
    {
        //Arrange
        dogRepositoryMock.Setup(x => x.Delete(It.IsAny<DogId>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        dogRepositoryMock.Verify(x => x.Delete(It.IsAny<DogId>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_delete_with_corect_values_when_handle(
        [Frozen] Mock<IDogRepository> dogRepositoryMock,
        DeleteDogCommand command,
        CancellationToken token,
        DeleteDogCommandHandler sut)
    {
        //Arrange
        DogId callbackDogId = null;
        dogRepositoryMock.Setup(x => x.Delete(It.IsAny<DogId>()))
            .Callback<DogId>((id) =>
            {
                callbackDogId = id;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackDogId.Should().Be(command.Id);
    }
}