using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Queries.Shows;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Queries.Shows;

public class GetShowQueryHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_GetById_from_repository_when_handle(
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        GetShowQuery query,
        Show show,
        CancellationToken token,
        GetShowQueryHandler sut)
    {
        //Arrange
        showRepositoryMock.Setup(x => x.GetById(It.IsAny<ShowId>())).ReturnsAsync(show);

        //Act
        var result = await sut.Handle(query, token);

        //Assert
        showRepositoryMock.Verify(x => x.GetById(It.IsAny<ShowId>()), Times.Once);
        result.Id.Should().Be(show.Id);
        result.Name.Should().Be(show.Name);
        result.Description.Should().Be(show.Description);
        result.Date.Should().Be(show.Date);
        result.Website.Should().Be(show.Website);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetById_with_corect_values_when_handle(
        [Frozen] Mock<IShowRepository> showRepositoryMock,
        GetShowQuery query,
        Show show,
        CancellationToken token,
        GetShowQueryHandler sut)
    {
        //Arrange
        ShowId callbackShowId = null;
        showRepositoryMock.Setup(x => x.GetById(It.IsAny<ShowId>()))
            .Callback<ShowId>((id) =>
            {
                callbackShowId = id;
            }).ReturnsAsync(show);

        //Act
        await sut.Handle(query, token);

        //Assert
        callbackShowId.Should().Be(query.Id);
    }
}