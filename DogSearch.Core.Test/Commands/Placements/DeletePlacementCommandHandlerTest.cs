using AutoFixture.Xunit2;
using DogSearch.Core.Commands.Placements;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands.Placements;

public class DeletePlacementCommandHandlerTest
{

    [Theory]
    [AutoMoqData]
    public async void Should_call_delete_from_repository_when_handle(
       [Frozen] Mock<IPlacementRepository> ownerRepositoryMock,
        DeletePlacementCommand command,
        CancellationToken token,
        DeletePlacementCommandHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.Delete(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.Delete(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_delete_with_corect_values_when_handle(
       [Frozen] Mock<IPlacementRepository> ownerRepositoryMock,
        DeletePlacementCommand command,
        CancellationToken token,
        DeletePlacementCommandHandler sut)
    {
        //Arrange
        DogId callbackDogId = null;
        ShowId callbackShowId = null;
        string callbackCategory = null;
        ownerRepositoryMock.Setup(x => x.Delete(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>()))
            .Callback<DogId, ShowId, string>((did, sid, cat) =>
            {
                callbackDogId = did;
                callbackShowId = sid;
                callbackCategory = cat;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackDogId.Should().Be(command.DogId);
        callbackShowId.Should().Be(command.ShowId);
        callbackCategory.Should().Be(command.Category);
    }
}
