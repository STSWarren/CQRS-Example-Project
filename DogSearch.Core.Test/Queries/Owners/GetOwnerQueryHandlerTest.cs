using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Queries.Owners;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Queries.Owners;

public class GetOwnerQueryHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_GetById_from_repository_when_handle(
        [Frozen] Mock<IOwnerRepository> ownerRepositoryMock,
        GetOwnerQuery query,
        Owner owner,
        CancellationToken token,
        GetOwnerQueryHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.GetById(It.IsAny<OwnerId>())).ReturnsAsync(owner);

        //Act
        var result = await sut.Handle(query, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.GetById(It.IsAny<OwnerId>()), Times.Once);
        result.Id.Should().Be(owner.Id);
        result.FirstName.Should().Be(owner.FirstName);
        result.LastName.Should().Be(owner.LastName);
        result.Email.Should().Be(owner.Email);
        result.Address.Should().Be(owner.Address);
        result.Phone.Should().Be(owner.Phone);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetById_with_corect_values_when_handle(
        [Frozen] Mock<IOwnerRepository> ownerRepositoryMock,
        GetOwnerQuery query,
        Owner owner,
        CancellationToken token,
        GetOwnerQueryHandler sut)
    {
        //Arrange
        OwnerId callbackOwnerId = null;
        ownerRepositoryMock.Setup(x => x.GetById(It.IsAny<OwnerId>()))
            .Callback<OwnerId>((id) =>
            {
                callbackOwnerId = id;
            }).ReturnsAsync(owner);

        //Act
        await sut.Handle(query, token);

        //Assert
        callbackOwnerId.Should().Be(query.Id);
    }
}
