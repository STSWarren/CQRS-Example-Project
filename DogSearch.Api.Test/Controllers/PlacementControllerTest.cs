using AutoFixture;
using DogSearch.Api.Controllers;
using DogSearch.Api.Dtos.Placements;
using DogSearch.Core.Commands.Placements;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Queries.Dogs;
using DogSearch.Core.Queries.Placements;
using DogSearch.Core.Queries.Shows;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DogSearch.Api.Test.Controllers;

public class PlacementControllerTest
{
    private PlacementController _sut;
    private Mock<IMediator> _mediatrMock;

    public PlacementControllerTest()
    {
        _mediatrMock = new Mock<IMediator>(MockBehavior.Strict);
        _sut = new PlacementController(_mediatrMock.Object);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_Queries_when_Get(
        IEnumerable<Placement> placements,
        IEnumerable<Dog> dogs,
        IEnumerable<Show> shows,
        Guid showId,
        Guid dogId, 
        string category)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Placement>>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(placements);
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Dog>>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(dogs);
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Show>>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(shows);

        //Act
        var result = await _sut.Get(showId, dogId, category);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<IEnumerable<Placement>>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<IEnumerable<Dog>>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<IEnumerable<Show>>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetAllPlacementsQuery_Query_when_GetAll(
        IEnumerable<Placement> placements,
        IEnumerable<Dog> dogs,
        IEnumerable<Show> shows)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Placement>>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(placements);
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Dog>>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(dogs);
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Show>>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(shows);

        //Act
        var result = await _sut.GetAll();

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<IEnumerable<Placement>>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<IEnumerable<Dog>>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<IEnumerable<Show>>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetAllPlacementsQuery_Query_with_correct_values_when_GetAll(
        IEnumerable<Placement> placements,
        IEnumerable<Dog> dogs,
        IEnumerable<Show> shows)
    {
        //Arrange
        IRequest<IEnumerable<Placement>> query = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Placement>>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<IEnumerable<Placement>>, CancellationToken>((req, _) =>
            {
                query = req;
            })
            .ReturnsAsync(placements);
        IRequest<IEnumerable<Dog>> listDogsQuery = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Dog>>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<IEnumerable<Dog>>, CancellationToken>((req, _) =>
            {
                listDogsQuery = req;
            })
            .ReturnsAsync(dogs);
        IRequest<IEnumerable<Show>> listShowsQuery = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Show>>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<IEnumerable<Show>>, CancellationToken>((req, _) =>
            {
                listShowsQuery = req;
            })
            .ReturnsAsync(shows);

        //Act
        var result = await _sut.GetAll();

        //Assert
        query.Should().BeOfType(typeof(GetAllPlacementsQuery));
        listDogsQuery.Should().BeOfType(typeof(ListDogsByIdQuery));
        listShowsQuery.Should().BeOfType(typeof(ListShowsByIdQuery));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_CreatePlacementCommand_Command_when_Create(
        CreatePlacementRequestDto dto)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Create(dto);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(CreatedAtActionResult));
        var created = result as CreatedAtActionResult;
        created.Value.Should().BeEquivalentTo(new
        {
            dogId = dto.DogId,
            showId = dto.ShowId,
            category = dto.Category,
            place = dto.Place
        });
        created.ActionName.Should().Be(nameof(_sut.Create));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_CreatePlacementCommand_Command_with_correct_values_when_Create(
        CreatePlacementRequestDto dto)
    {
        //Arrange
        CreatePlacementCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest, CancellationToken>((req, _) =>
            {
                command = req as CreatePlacementCommand;
            })
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Create(dto);

        //Assert
        command.ShowId.Value.Should().Be(dto.ShowId);
        command.DogId.Value.Should().Be(dto.DogId);
        command.Category.Should().Be(dto.Category);
        command.Place.Should().Be(dto.Place);

    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_UpdatePlacementCommand_Command_when_Update(
        UpdatePlacementRequestDto dto)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Update(dto);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_UpdatePlacementCommand_Command_when_Update_Command_with_correct_values_when_Update(
        UpdatePlacementRequestDto dto)
    {
        //Arrange
        UpdatePlacementCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest, CancellationToken>((req, _) =>
            {
                command = req as UpdatePlacementCommand;
            })
            .Returns(Task.CompletedTask);

        //Act
        _ = await _sut.Update(dto);

        //Assert
        command.ShowId.Value.Should().Be(dto.ShowId);
        command.DogId.Value.Should().Be(dto.DogId);
        command.Category.Should().Be(dto.Category);
        command.Place.Should().Be(dto.Place);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_DeletePlacementCommand_Command_when_Delete(
        DeletePlacementRequestDto dto)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Delete(dto);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_DeletePlacementCommand_Command_with_correct_values_when_Delete(
        DeletePlacementRequestDto dto)
    {
        //Arrange
        DeletePlacementCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest, CancellationToken>((req, _) =>
            {
                command = req as DeletePlacementCommand;
            })
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Delete(dto);

        //Assert
        command.ShowId.Value.Should().Be(dto.ShowId);
        command.DogId.Value.Should().Be(dto.DogId);
        command.Category.Should().Be(dto.Category);
    }
}
