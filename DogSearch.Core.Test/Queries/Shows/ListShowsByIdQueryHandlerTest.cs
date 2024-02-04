using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Queries.Shows;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Queries.Shows;

public class ListShowsByIdQueryHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_ListByIds_from_repository_when_handle(
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        ListShowsByIdQuery query,
        IEnumerable<Show> shows,
        CancellationToken token,
        ListShowsByIdQueryHandler sut)
    {
        //Arrange
        showRepositoryMock.Setup(x => x.ListByIds(It.IsAny<IEnumerable<ShowId>>())).ReturnsAsync(shows);

        //Act
        var result = await sut.Handle(query, token);

        //Assert
        showRepositoryMock.Verify(x => x.ListByIds(It.IsAny<IEnumerable<ShowId>>()), Times.Once);
        result.Should().BeEquivalentTo(shows);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_ListByIds_with_corect_values_when_handle(
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        ListShowsByIdQuery query,
        IEnumerable<Show> shows,
        CancellationToken token,
        ListShowsByIdQueryHandler sut)
    {
        //Arrange
        IEnumerable<ShowId> callbackShowId = null;
        showRepositoryMock.Setup(x => x.ListByIds(It.IsAny<IEnumerable<ShowId>>()))
            .Callback<IEnumerable<ShowId>>((ids) =>
            {
                callbackShowId = ids;
            }).ReturnsAsync(shows);

        //Act
        await sut.Handle(query, token);

        //Assert
        callbackShowId.Should().BeEquivalentTo(query.Ids);
    }
}
