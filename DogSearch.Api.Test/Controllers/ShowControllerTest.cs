using AutoFixture;
using DogSearch.Api.Controllers;
using DogSearch.Api.Dtos.Shows;
using DogSearch.Core.Commands.Shows;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Queries.Shows;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DogSearch.Api.Test.Controllers;

public class ShowControllerTest
{
    private ShowController _sut;
    private Mock<IMediator> _mediatrMock;

    public ShowControllerTest()
    {
        _mediatrMock = new Mock<IMediator>(MockBehavior.Strict);
        _sut = new ShowController(_mediatrMock.Object);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetShowQuery_Query_when_Get(
        Guid id,
        Show show)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<Show>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(show);

        //Act
        var result = await _sut.Get(id);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<Show>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(OkObjectResult));
    }


    [Theory]
    [AutoMoqData]
    public async void Should_send_GetShowQuery_Query_with_correct_values_when_Get(
        Guid id,
        Show show)
    {
        //Arrange
        GetShowQuery query = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<Show>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<Show>, CancellationToken>((req, _) =>
            {
                query = req as GetShowQuery;
            })
            .ReturnsAsync(show);

        //Act
        var result = await _sut.Get(id);

        //Assert
        query.Id.Value.Should().Be(id);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetAllShowsQuery_Query_when_GetAll(
        IEnumerable<Show> shows)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Show>>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(shows);

        //Act
        var result = await _sut.GetAll();

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<IEnumerable<Show>>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetAllShowsQuery_Query_with_correct_values_when_GetAll(
        IEnumerable<Show> shows)
    {
        //Arrange
        IRequest<IEnumerable<Show>> query = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Show>>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<IEnumerable<Show>>, CancellationToken>((req, _) =>
            {
                query = req;
            })
            .ReturnsAsync(shows);

        //Act
        var result = await _sut.GetAll();

        //Assert
        query.Should().BeOfType(typeof(GetAllShowsQuery));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_CreateShowCommand_Command_when_Create(
        Fixture fixture,
        ShowId showId)
    {
        //Arrange
        var dto = fixture.Build<CreateShowRequestDto>().With(x => x.Date, "11.01.2024").Create();
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<ShowId>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(showId);

        //Act
        var result = await _sut.Create(dto);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<ShowId>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(CreatedAtActionResult));
        var created = result as CreatedAtActionResult;
        created.Value.Should().BeEquivalentTo(new { id = showId.Value });
        created.ActionName.Should().Be(nameof(_sut.Create));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_CreateShowCommand_Command_with_correct_values_when_Create(
        Fixture fixture,
        ShowId ShowId)
    {
        //Arrange
        var dto = fixture.Build<CreateShowRequestDto>().With(x=>x.Date, "11.01.2024").Create();
        CreateShowCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<ShowId>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<ShowId>, CancellationToken>((req, _) =>
            {
                command = req as CreateShowCommand;
            })
            .ReturnsAsync(ShowId);

        //Act
        var result = await _sut.Create(dto);

        //Assert
        command.Name.Should().Be(dto.Name);
        command.Description.Should().Be(dto.Description);
        command.Date.ToString("dd.MM.yyyy").Should().Be("11.01.2024");
        command.Website.Should().Be(dto.Website);

    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_UpdateShowCommand_Command_when_Update(
        Fixture fixture,
        ShowId showId)
    {
        //Arrange
        var dto = fixture.Build<UpdateShowRequestDto>().With(x => x.Date, "11.01.2024").Create();
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Update(showId.Value, dto);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_UpdateShowCommand_Command_when_Update_Command_with_correct_values_when_Update(
        Fixture fixture,
        ShowId showId)
    {
        //Arrange
        var dto = fixture.Build<UpdateShowRequestDto>().With(x => x.Date, "11.01.2024").Create();
        UpdateShowCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest, CancellationToken>((req, _) =>
            {
                command = req as UpdateShowCommand;
            })
            .Returns(Task.CompletedTask);

        //Act
        _ = await _sut.Update(showId.Value, dto);

        //Assert
        command.Id.Value.Should().Be(showId.Value);
        command.Name.Should().Be(dto.Name);
        command.Description.Should().Be(dto.Description);
        command.Date.ToString("dd.MM.yyyy").Should().Be("11.01.2024");
        command.Website.Should().Be(dto.Website);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_DeleteShowCommand_Command_when_Delete(
        Guid showId)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Delete(showId);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_DeleteShowCommand_Command_with_correct_values_when_Delete(
        Guid showId)
    {
        //Arrange
        DeleteShowCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest, CancellationToken>((req, _) =>
            {
                command = req as DeleteShowCommand;
            })
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Delete(showId);

        //Assert
        command.Id.Value.Should().Be(showId);
    }
}
