using AutoFixture.Xunit2;
using DogSearch.Core.Commands.Owners;
using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands.Owners;

public class CreateOwnerCommandHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_create_from_repository_when_handle(
       [Frozen] Mock<IOwnerRepository> ownerRepositoryMock,
       CreateOwnerCommand command,
       CancellationToken token,
       CreateOwnerCommandHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.Create(It.IsAny<Owner>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.Create(It.IsAny<Owner>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_create_with_corect_values_when_handle(
       [Frozen] Mock<IOwnerRepository> ownerRepositoryMock,
        CreateOwnerCommand command,
        CancellationToken token,
        CreateOwnerCommandHandler sut)
    {
        //Arrange
        Owner callbackOwner = null;
        ownerRepositoryMock.Setup(x => x.Create(It.IsAny<Owner>()))
            .Callback<Owner>((o) =>
            {
                callbackOwner = o;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackOwner.FirstName.Should().Be(command.FirstName);
        callbackOwner.LastName.Should().Be(command.LastName);
        callbackOwner.Address.Should().Be(command.Address);
        callbackOwner.Email.Should().Be(command.Email);
        callbackOwner.Phone.Should().Be(command.Phone);
    }
}
