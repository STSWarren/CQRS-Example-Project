using AutoFixture.Xunit2;
using DogSearch.Core.Commands.Shows;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands.Shows;

public class CreateShowCommandHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_create_from_repository_when_handle(
       [Frozen] Mock<IShowRepository> ownerRepositoryMock,
       CreateShowCommand command,
       CancellationToken token,
       CreateShowCommandHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.Create(It.IsAny<Show>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.Create(It.IsAny<Show>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_create_with_corect_values_when_handle(
       [Frozen] Mock<IShowRepository> ownerRepositoryMock,
        CreateShowCommand command,
        CancellationToken token,
        CreateShowCommandHandler sut)
    {
        //Arrange
        Show callbackShow = null;
        ownerRepositoryMock.Setup(x => x.Create(It.IsAny<Show>()))
            .Callback<Show>((o) =>
            {
                callbackShow = o;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackShow.Name.Should().Be(command.Name);
        callbackShow.Description.Should().Be(command.Description);
        callbackShow.Date.Should().Be(command.Date);
        callbackShow.Website.Should().Be(command.Website);
    }
}
