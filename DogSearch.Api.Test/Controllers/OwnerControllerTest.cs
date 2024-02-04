using AutoFixture;
using DogSearch.Api.Controllers;
using DogSearch.Api.Dtos.Owners;
using DogSearch.Core.Commands.Owners;
using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Queries.Owners;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DogSearch.Api.Test.Controllers;

public class OwnerControllerTest
{
    private OwnerController _sut;
    private Mock<IMediator> _mediatrMock;

    public OwnerControllerTest()
    {
        _mediatrMock = new Mock<IMediator>(MockBehavior.Strict);
        _sut = new OwnerController(_mediatrMock.Object);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetOwnerQuery_Query_when_Get(
        Guid id,
        Owner owner)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<Owner>>(),
            It.IsAny<CancellationToken>())).ReturnsAsync(owner);

        //Act
        var result = await _sut.Get(id);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<Owner>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(OkObjectResult));
    }


    [Theory]
    [AutoMoqData]
    public async void Should_send_GetOwnerQuery_Query_with_correct_values_when_Get(
        Guid id,
        Owner owner)
    {
        //Arrange
        GetOwnerQuery query = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<Owner>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<Owner>, CancellationToken>((req, _) =>
            {
                query = req as GetOwnerQuery;
            })
            .ReturnsAsync(owner);

        //Act
        var result = await _sut.Get(id);

        //Assert
        query.Id.Value.Should().Be(id);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetAllOwnersQuery_Query_when_GetAll(
        IEnumerable<Owner> owners)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Owner>>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(owners);

        //Act
        var result = await _sut.GetAll();

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<IEnumerable<Owner>>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetAllOwnersQuery_Query_with_correct_values_when_GetAll(
        IEnumerable<Owner> owners)
    {
        //Arrange
        IRequest<IEnumerable<Owner>> query = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Owner>>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<IEnumerable<Owner>>, CancellationToken>((req, _) =>
            {
                query = req;
            })
            .ReturnsAsync(owners);

        //Act
        var result = await _sut.GetAll();

        //Assert
        query.Should().BeOfType(typeof(GetAllOwnersQuery));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_CreateOwnerCommand_Command_when_Create(
        CreateOwnerRequestDto dto,
        OwnerId ownerId)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<OwnerId>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(ownerId);

        //Act
        var result = await _sut.Create(dto);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<OwnerId>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(CreatedAtActionResult));
        var created = result as CreatedAtActionResult;
        created.Value.Should().BeEquivalentTo(new { id = ownerId.Value });
        created.ActionName.Should().Be(nameof(_sut.Create));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_CreateOwnerCommand_Command_with_correct_values_when_Create(
        CreateOwnerRequestDto dto,
        OwnerId OwnerId)
    {
        //Arrange
        CreateOwnerCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<OwnerId>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<OwnerId>, CancellationToken>((req, _) =>
            {
                command = req as CreateOwnerCommand;
            })
            .ReturnsAsync(OwnerId);

        //Act
        var result = await _sut.Create(dto);

        //Assert
        command.FirstName.Should().Be(dto.FirstName);
        command.LastName.Should().Be(dto.LastName);
        command.Email.Should().Be(dto.Email);
        command.Address.Should().Be(dto.Address);
        command.Phone.Should().Be(dto.Phone);

    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_UpdateOwnerCommand_Command_when_Update(
        UpdateOwnerRequestDto dto,
        OwnerId ownerId)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Update(ownerId.Value, dto);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_UpdateOwnerCommand_Command_when_Update_Command_with_correct_values_when_Update(
        UpdateOwnerRequestDto dto,
        OwnerId ownerId)
    {
        //Arrange
        UpdateOwnerCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest, CancellationToken>((req, _) =>
            {
                command = req as UpdateOwnerCommand;
            })
            .Returns(Task.CompletedTask);

        //Act
        _ = await _sut.Update(ownerId.Value, dto);

        //Assert
        command.Id.Value.Should().Be(ownerId.Value);
        command.FirstName.Should().Be(dto.FirstName);
        command.LastName.Should().Be(dto.LastName);
        command.Email.Should().Be(dto.Email);
        command.Address.Should().Be(dto.Address);
        command.Phone.Should().Be(dto.Phone);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_DeleteOwnerCommand_Command_when_Delete(
        Guid ownerId)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Delete(ownerId);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_DeleteOwnerCommand_Command_with_correct_values_when_Delete(
        Guid ownerId)
    {
        //Arrange
        DeleteOwnerCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest, CancellationToken>((req, _) =>
            {
                command = req as DeleteOwnerCommand;
            })
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Delete(ownerId);

        //Assert
        command.Id.Value.Should().Be(ownerId);
    }
}
