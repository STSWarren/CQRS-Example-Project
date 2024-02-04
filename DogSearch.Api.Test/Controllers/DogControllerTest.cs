using AutoFixture;
using DogSearch.Api.Dtos.Dogs;
using DogSearch.Application.Controllers;
using DogSearch.Core.Commands.Dogs;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Queries.Dogs;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace DogSearch.Api.Test.Controllers;

public class DogControllerTest
{
    private DogController _sut;
    private Mock<IMediator> _mediatrMock;

    public DogControllerTest(){
        _mediatrMock = new Mock<IMediator>(MockBehavior.Strict);
        _sut = new DogController(_mediatrMock.Object);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetDogQuery_Query_when_Get(
        Guid id,
        Dog dog)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<Dog>>(), 
            It.IsAny<CancellationToken>())).ReturnsAsync(dog);

        //Act
        var result = await _sut.Get(id);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<Dog>>(), 
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(OkObjectResult));
    }


    [Theory]
    [AutoMoqData]
    public async void Should_send_GetDogQuery_Query_with_correct_values_when_Get(
        Guid id,
        Dog dog)
    {
        //Arrange
        GetDogQuery query = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<Dog>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<Dog>, CancellationToken>((req, _) =>
            {
                query = req as GetDogQuery;
            })
            .ReturnsAsync(dog);

        //Act
        var result = await _sut.Get(id);

        //Assert
        query.Id.Value.Should().Be(id);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetAllDogsQuery_Query_when_GetAll(
        IEnumerable<Dog> dogs)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Dog>>>(), 
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(dogs);

        //Act
        var result = await _sut.GetAll();

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<IEnumerable<Dog>>>(), 
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(OkObjectResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_GetAllDogsQuery_Query_with_correct_values_when_GetAll(
        IEnumerable<Dog> dogs)
    {
        //Arrange
        IRequest<IEnumerable<Dog>> query = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<IEnumerable<Dog>>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<IEnumerable<Dog>>, CancellationToken>((req, _) =>
            {
                query = req;
            })
            .ReturnsAsync(dogs);

        //Act
        var result = await _sut.GetAll();

        //Assert
        query.Should().BeOfType(typeof(GetAllDogsQuery));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_CreateDogCommand_Command_when_Create(
        Fixture fixture,
        Size size,
        DogId dogId)
    {
        //Arrange
        var dto = fixture.Build<CreateDogRequestDto>().With(x=>x.Size, size.ToString()).Create();
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<DogId>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(dogId);

        //Act
        var result = await _sut.Create(dto);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest<DogId>>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(CreatedAtActionResult));
        var created = result as CreatedAtActionResult;
        created.Value.Should().BeEquivalentTo(new { id = dogId.Value });
        created.ActionName.Should().Be(nameof(_sut.Create));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_CreateDogCommand_Command_with_correct_values_when_Create(
        Fixture fixture,
        Size size,
        DogId dogId)
    {
        //Arrange
        var dto = fixture.Build<CreateDogRequestDto>().With(x => x.Size, size.ToString()).Create();
        CreateDogCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest<DogId>>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest<DogId>, CancellationToken>((req, _) =>
            {
                command = req as CreateDogCommand;
            })
            .ReturnsAsync(dogId);

        //Act
        var result = await _sut.Create(dto);

        //Assert
        command.Breed.Should().Be(dto.Breed);
        command.Name.Should().Be(dto.Name);
        command.OwnerId.Should().Be(dto.OwnerId);
        command.Size.Should().Be(size);

    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_UpdateDogCommand_Command_when_Update(
        Fixture fixture,
        Size size,
        DogId dogId)
    {
        //Arrange
        var dto = fixture.Build<UpdateDogRequestDto>().With(x => x.Size, size.ToString()).Create();
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Update(dogId.Value, dto);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_UpdateDogCommand_Command_when_Update_Command_with_correct_values_when_Update(
        Fixture fixture,
        Size size,
        DogId dogId)
    {
        //Arrange
        var dto = fixture.Build<UpdateDogRequestDto>().With(x => x.Size, size.ToString()).Create();
        UpdateDogCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest, CancellationToken>((req, _) =>
            {
                command = req as UpdateDogCommand;
            })
            .Returns(Task.CompletedTask);

        //Act
        _ = await _sut.Update(dogId.Value, dto);

        //Assert
        command.Id.Value.Should().Be(dogId.Value);
        command.Breed.Should().Be(dto.Breed);
        command.Name.Should().Be(dto.Name);
        command.OwnerId.Should().Be(dto.OwnerId.Value);
        command.Size.Should().Be(size);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_DeleteDogCommand_Command_when_Delete(
        Guid dogId)
    {
        //Arrange
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Delete(dogId);

        //Assert
        _mediatrMock.Verify(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()), Times.Once);
        result.Should().BeOfType(typeof(NoContentResult));
    }

    [Theory]
    [AutoMoqData]
    public async void Should_send_DeleteDogCommand_Command_with_correct_values_when_Delete(
        Guid dogId)
    {
        //Arrange
        DeleteDogCommand command = null;
        _mediatrMock.Setup(x => x.Send(It.IsAny<IRequest>(),
            It.IsAny<CancellationToken>()))
            .Callback<IRequest, CancellationToken>((req, _) =>
            {
                command = req as DeleteDogCommand;
            })
            .Returns(Task.CompletedTask);

        //Act
        var result = await _sut.Delete(dogId);

        //Assert
        command.Id.Value.Should().Be(dogId);
    }
}