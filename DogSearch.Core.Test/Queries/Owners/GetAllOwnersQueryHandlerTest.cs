using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Queries.Owners;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Queries.Owners;

public class GetAllOwnersQueryHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAll_from_repository_when_handle(
        [Frozen] Mock<IOwnerRepository> ownerRepositoryMock,
        GetAllOwnersQuery query,
        IEnumerable<Owner> owners,
        CancellationToken token,
        GetAllOwnersQueryHandler sut)
    {
        //Arrange
        ownerRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(owners);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        ownerRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        results.Should().BeEquivalentTo(owners);
    }
}
