using AutoFixture.Xunit2;
using DogSearch.Core.Commands.Shows;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands.Shows;

public class DeleteShowCommandHandlerTest
{

    [Theory]
    [AutoMoqData]
    public async void Should_call_delete_from_repository_when_handle(
       [Frozen] Mock<IShowRepository> ownerRepositoryMock,
        DeleteShowCommand command,
        CancellationToken token,
        DeleteShowCommandHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.Delete(It.IsAny<ShowId>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.Delete(It.IsAny<ShowId>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_delete_with_corect_values_when_handle(
       [Frozen] Mock<IShowRepository> ownerRepositoryMock,
        DeleteShowCommand command,
        CancellationToken token,
        DeleteShowCommandHandler sut)
    {
        //Arrange
        ShowId callbackShowId = null;
        ownerRepositoryMock.Setup(x => x.Delete(It.IsAny<ShowId>()))
            .Callback<ShowId>((id) =>
            {
                callbackShowId = id;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackShowId.Should().Be(command.Id);
    }
}
