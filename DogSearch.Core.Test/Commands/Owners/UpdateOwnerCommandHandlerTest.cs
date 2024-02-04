using AutoFixture.Xunit2; 
using DogSearch.Core.Commands.Owners;
using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands.Owners;

public class UpdateOwnerCommandHandlerTest
{

    [Theory]
    [AutoMoqData]
    public async void Should_call_Update_from_repository_when_handle(
       [Frozen] Mock<IOwnerRepository> ownerRepositoryMock,
        UpdateOwnerCommand command,
        CancellationToken token,
        UpdateOwnerCommandHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.Update(It.IsAny<OwnerId>(), It.IsAny<Owner>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.Update(It.IsAny<OwnerId>(), It.IsAny<Owner>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_Update_with_corect_values_when_handle(
       [Frozen] Mock<IOwnerRepository> ownerRepositoryMock,
        UpdateOwnerCommand command,
        CancellationToken token,
        UpdateOwnerCommandHandler sut)
    {
        //Arrange
        Owner callbackOwner = null;
        OwnerId callbackOwnerId = null;
        ownerRepositoryMock.Setup(x => x.Update(It.IsAny<OwnerId>(), It.IsAny<Owner>()))
            .Callback<OwnerId, Owner>((id, o) =>
            {
                callbackOwnerId = id;
                callbackOwner = o;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackOwnerId.Should().Be(command.Id);
        callbackOwner.FirstName.Should().Be(command.FirstName);
        callbackOwner.LastName.Should().Be(command.LastName);
        callbackOwner.Address.Should().Be(command.Address);
        callbackOwner.Email.Should().Be(command.Email);
        callbackOwner.Phone.Should().Be(command.Phone);
    }
}
