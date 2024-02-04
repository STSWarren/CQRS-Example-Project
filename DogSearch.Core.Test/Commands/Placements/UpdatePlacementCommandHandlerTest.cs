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

public class UpdatePlacementCommandHandlerTest
{

    [Theory]
    [AutoMoqData]
    public async void Should_call_Update_from_repository_when_handle(
       [Frozen] Mock<IPlacementRepository> ownerRepositoryMock,
        UpdatePlacementCommand command,
        CancellationToken token,
        UpdatePlacementCommandHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.Update(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>(), It.IsAny<int>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.Update(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_Update_with_corect_values_when_handle(
       [Frozen] Mock<IPlacementRepository> ownerRepositoryMock,
        UpdatePlacementCommand command,
        CancellationToken token,
        UpdatePlacementCommandHandler sut)
    {
        //Arrange
        DogId callbackDogId = null;
        ShowId callbackShowId = null;
        string callbackCategory = null;
        int place = 0;
        ownerRepositoryMock.Setup(x => x.Update(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>(), It.IsAny<int>()))
            .Callback<DogId, ShowId, string, int>((did, sid, cat, p) =>
            {
                callbackDogId = did;
                callbackShowId = sid;
                callbackCategory = cat;
                place = p;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackDogId.Should().Be(command.DogId);
        callbackShowId.Should().Be(command.ShowId);
        callbackCategory.Should().Be(command.Category);
        place.Should().Be(command.Place);
    }
}
