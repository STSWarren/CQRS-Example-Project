using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Queries.Shows;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Queries.Shows;

public class GetAll_ShowsQueryHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAll_from_repository_when_handle(
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        GetAllShowsQuery query,
        IEnumerable<Show> shows,
        CancellationToken token,
        GetAllShowsQueryHandler sut)
    {
        //Arrange
        showRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(shows);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        showRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        results.Should().BeEquivalentTo(shows);
    }
}
