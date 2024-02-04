using AutoFixture.Xunit2;
using DogSearch.Core.Commands.Placements;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Commands.Placements;

public class CreatePlacementCommandHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_create_from_repository_when_handle(
       [Frozen] Mock<IPlacementRepository> ownerRepositoryMock,
       CreatePlacementCommand command,
       CancellationToken token,
       CreatePlacementCommandHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.Create(It.IsAny<Placement>())).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.Create(It.IsAny<Placement>()), Times.Once);
    }


    [Theory]
    [AutoMoqData]
    public async void Should_call_create_with_corect_values_when_handle(
       [Frozen] Mock<IPlacementRepository> ownerRepositoryMock,
        CreatePlacementCommand command,
        CancellationToken token,
        CreatePlacementCommandHandler sut)
    {
        //Arrange
        Placement callbackPlacement = null;
        ownerRepositoryMock.Setup(x => x.Create(It.IsAny<Placement>()))
            .Callback<Placement>((o) =>
            {
                callbackPlacement = o;
            }).Returns(Task.CompletedTask);

        //Act
        await sut.Handle(command, token);

        //Assert
        callbackPlacement.DogId.Should().Be(command.DogId);
        callbackPlacement.ShowId.Should().Be(command.ShowId);
        callbackPlacement.Category.Should().Be(command.Category);
        callbackPlacement.Place.Should().Be(command.Place);
    }
}
