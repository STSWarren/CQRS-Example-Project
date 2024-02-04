using AutoFixture;
using AutoFixture.Xunit2;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Entities.Shows;
using DogSearch.Core.Interfaces.Infrastructure.Repositories;
using DogSearch.Core.Queries.Placements;
using DogSearch.Core.Test.TestingAttributes;
using FluentAssertions;
using Moq;
using Xunit;

namespace DogSearch.Core.Test.Queries.Placements;

public class ListPlacementsQueryHandlerTest
{
    [Theory]
    [AutoMoqData]
    public async void Should_call_Get_from_repository_when_all_filter_parameters_are_present_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        ListPlacementsQuery query,
        Placement placement,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        placementRepositoryMock.Setup(x => x.Get(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>())).ReturnsAsync(placement);

        //Act
        var result = await sut.Handle(query, token);

        //Assert
        placementRepositoryMock.Verify(x => x.Get(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>()), Times.Once);
        result.Should().BeEquivalentTo(new List<Placement> { placement });
    }
    [Theory]
    [AutoMoqData]
    public async void Should_call_Get_from_repository_with_correct_values_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        ListPlacementsQuery query,
        Placement placement,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        DogId callbackDogId = null;
        ShowId callbackShowId = null;
        string callbackCategory = null;
        placementRepositoryMock.Setup(x => x.Get(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>()))
            .Callback<DogId, ShowId, string>((did, sid, cat) =>
            {
                callbackDogId = did;
                callbackShowId = sid;
                callbackCategory = cat;
            })
            .ReturnsAsync(placement);

        //Act
        var result = await sut.Handle(query, token);

        //Assert
        callbackDogId.Should().Be(query.DogId);
        callbackShowId.Should().Be(query.ShowId);
        callbackCategory.Should().Be(query.Category);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAllByShowIdAndDogId_from_repository_when_category_is_empty_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        Fixture fixture,
        IEnumerable<Placement> placements,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        var query = fixture.Build<ListPlacementsQuery>().With(x => x.Category, string.Empty).Create();
        placementRepositoryMock.Setup(x => x.GetAllByShowIdAndDogId(It.IsAny<ShowId>(), It.IsAny<DogId>())).ReturnsAsync(placements);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        placementRepositoryMock.Verify(x => x.GetAllByShowIdAndDogId(It.IsAny<ShowId>(), It.IsAny<DogId>()), Times.Once);
        results.Should().BeEquivalentTo(placements);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAllByShowIdAndDogId_from_repository_with_correct_values_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        Fixture fixture,
        IEnumerable<Placement> placements,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        DogId callbackDogId = null;
        ShowId callbackShowId = null;
        var query = fixture.Build<ListPlacementsQuery>().With(x => x.Category, string.Empty).Create();
        placementRepositoryMock.Setup(x => x.GetAllByShowIdAndDogId(It.IsAny<ShowId>(), It.IsAny<DogId>()))
            .Callback<ShowId, DogId>((sid, did) =>
            {
                callbackDogId = did;
                callbackShowId = sid;
            }).ReturnsAsync(placements);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        callbackDogId.Should().Be(query.DogId);
        callbackShowId.Should().Be(query.ShowId);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAllByShowIdAndCategory_from_repository_when_DogId_is_empty_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        Fixture fixture,
        IEnumerable<Placement> placements,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        var query = fixture.Build<ListPlacementsQuery>().With(x => x.DogId, new DogId(Guid.Empty)).Create();
        placementRepositoryMock.Setup(x => x.GetAllByShowIdAndCategory(It.IsAny<ShowId>(), It.IsAny<string>())).ReturnsAsync(placements);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        placementRepositoryMock.Verify(x => x.GetAllByShowIdAndCategory(It.IsAny<ShowId>(), It.IsAny<string>()), Times.Once);
        results.Should().BeEquivalentTo(placements);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAllByShowIdAndCategory_from_repository_with_correct_values_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        Fixture fixture,
        IEnumerable<Placement> placements,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        ShowId callbackShowId = null;
        string callbackCategory = null;
        var query = fixture.Build<ListPlacementsQuery>().With(x => x.DogId, new DogId(Guid.Empty)).Create();
        placementRepositoryMock.Setup(x => x.GetAllByShowIdAndCategory(It.IsAny<ShowId>(), It.IsAny<string>()))
            .Callback< ShowId, string>(( sid, cat) =>
            {
                callbackShowId = sid;
                callbackCategory = cat;
            }).ReturnsAsync(placements);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        callbackShowId.Should().Be(query.ShowId);
        callbackCategory.Should().Be(query.Category);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAllByDogId_from_repository_when_ShowId_is_empty_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        Fixture fixture,
        IEnumerable<Placement> placements,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        var query = fixture.Build<ListPlacementsQuery>().With(x => x.ShowId, new ShowId(Guid.Empty)).Create();
        placementRepositoryMock.Setup(x => x.GetAllByDogId(It.IsAny<DogId>())).ReturnsAsync(placements);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        placementRepositoryMock.Verify(x => x.GetAllByDogId(It.IsAny<DogId>()), Times.Once);
        results.Should().BeEquivalentTo(placements);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAllByDogId_from_repository_with_correct_values_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        Fixture fixture,
        IEnumerable<Placement> placements,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        DogId callbackDogId = null;
        var query = fixture.Build<ListPlacementsQuery>().With(x => x.ShowId, new ShowId(Guid.Empty)).Create();
        placementRepositoryMock.Setup(x => x.GetAllByDogId(It.IsAny<DogId>()))
            .Callback<DogId>((did) =>
            {
                callbackDogId = did;
            }).ReturnsAsync(placements);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        callbackDogId.Should().Be(query.DogId);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAllByShowId_from_repository_when_DogId_and_Category_is_empty_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        Fixture fixture,
        IEnumerable<Placement> placements,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        var query = fixture.Build<ListPlacementsQuery>().With(x => x.DogId, new DogId(Guid.Empty)).With(x => x.Category, string.Empty).Create();
        placementRepositoryMock.Setup(x => x.GetAllByShowId(It.IsAny<ShowId>())).ReturnsAsync(placements);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        placementRepositoryMock.Verify(x => x.GetAllByShowId(It.IsAny<ShowId>()), Times.Once);
        results.Should().BeEquivalentTo(placements);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_call_GetAllByShowId_from_repository_with_correct_values_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        Fixture fixture,
        IEnumerable<Placement> placements,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        ShowId callbackShowId = null;
        var query = fixture.Build<ListPlacementsQuery>().With(x => x.DogId, new DogId(Guid.Empty)).With(x => x.Category, string.Empty).Create();
        placementRepositoryMock.Setup(x => x.GetAllByShowId(It.IsAny<ShowId>()))
            .Callback<ShowId>((sid) =>
            {
                callbackShowId = sid;
            }).ReturnsAsync(placements);

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        callbackShowId.Should().Be(query.ShowId);
    }

    [Theory]
    [AutoMoqData]
    public async void Should_return_empty_list_when_all_filters_are_empty_when_handle(
        [Frozen] Mock<IPlacementRepository> placementRepositoryMock,
        Fixture fixture,
        CancellationToken token,
        ListPlacementsQueryHandler sut)
    {
        //Arrange
        var query = fixture.Build<ListPlacementsQuery>()
            .With(x => x.DogId, new DogId(Guid.Empty))
            .With(x => x.ShowId, new ShowId(Guid.Empty))
            .With(x => x.Category, string.Empty).Create();

        //Act
        var results = await sut.Handle(query, token);

        //Assert
        placementRepositoryMock.Verify(x => x.Get(It.IsAny<DogId>(), It.IsAny<ShowId>(), It.IsAny<string>()), Times.Never);
        placementRepositoryMock.Verify(x => x.GetAllByShowIdAndDogId(It.IsAny<ShowId>(), It.IsAny<DogId>()), Times.Never);
        placementRepositoryMock.Verify(x => x.GetAllByShowIdAndDogId(It.IsAny<ShowId>(), It.IsAny<DogId>()), Times.Never);
        placementRepositoryMock.Verify(x => x.GetAllByShowIdAndCategory(It.IsAny<ShowId>(), It.IsAny<string>()), Times.Never);
        placementRepositoryMock.Verify(x => x.GetAllByDogId(It.IsAny<DogId>()), Times.Never);
        placementRepositoryMock.Verify(x => x.GetAllByShowId(It.IsAny<ShowId>()), Times.Never);
        results.Should().BeEquivalentTo(new List<Placement>());
    }
}