using AutoFixture.Xunit2;
using DogSearch.Core.Commands.Owners;
using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands.Owners;

public class DeleteOwnerCommandHandlerTest
{

    [Theory]
    [AutoMoqData]
    public async void Should_call_delete_from_repository_when_handle(
       [Frozen] Mock<IOwnerRepository> ownerRepositoryMock,
        DeleteOwnerCommand command,
        CancellationToken token,
        DeleteOwnerCommandHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.Delete(It.IsAny<OwnerId>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.Delete(It.IsAny<OwnerId>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_delete_with_corect_values_when_handle(
       [Frozen] Mock<IOwnerRepository> ownerRepositoryMock,
        DeleteOwnerCommand command,
        CancellationToken token,
        DeleteOwnerCommandHandler sut)
    {
        //Arrange
        OwnerId callbackOwnerId = null;
        ownerRepositoryMock.Setup(x => x.Delete(It.IsAny<OwnerId>()))
            .Callback<OwnerId>((id) =>
            {
                callbackOwnerId = id;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackOwnerId.Should().Be(command.Id);
    }
}
