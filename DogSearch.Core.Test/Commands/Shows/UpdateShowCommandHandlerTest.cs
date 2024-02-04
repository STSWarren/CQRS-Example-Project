using AutoFixture.Xunit2;
using DogSearch.Core.Commands.Shows;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands.Shows;

public class UpdateShowCommandHandlerTest
{

    [Theory]
    [AutoMoqData]
    public async void Should_call_Update_from_repository_when_handle(
       [Frozen] Mock<IShowRepository> ownerRepositoryMock,
        UpdateShowCommand command,
        CancellationToken token,
        UpdateShowCommandHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.Update(It.IsAny<ShowId>(), It.IsAny<Show>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.Update(It.IsAny<ShowId>(), It.IsAny<Show>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_Update_with_corect_values_when_handle(
       [Frozen] Mock<IShowRepository> ownerRepositoryMock,
        UpdateShowCommand command,
        CancellationToken token,
        UpdateShowCommandHandler sut)
    {
        //Arrange
        Show callbackShow = null;
        ShowId callbackShowId = null;
        ownerRepositoryMock.Setup(x => x.Update(It.IsAny<ShowId>(), It.IsAny<Show>()))
            .Callback<ShowId, Show>((id, o) =>
            {
                callbackShowId = id;
                callbackShow = o;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackShowId.Should().Be(command.Id);
        callbackShow.Name.Should().Be(command.Name);
        callbackShow.Description.Should().Be(command.Description);
        callbackShow.Date.Should().Be(command.Date);
        callbackShow.Website.Should().Be(command.Website);
    }
}
