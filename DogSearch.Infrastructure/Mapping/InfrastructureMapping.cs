using AutoMapper;
using DogSearch.Core.Entities.Dog;
using DogSearch.Core.Entities.Owners;
using DogSearch.Core.Entities.Placements;
using DogSearch.Core.Entities.Shows;
using DogSearch.Infrastructure.Dtos.Dogs;
using DogSearch.Infrastructure.Dtos.Owners;
using DogSearch.Infrastructure.Dtos.Placements;
using DogSearch.Infrastructure.Dtos.Shows;

namespace DogSearch.Infrastructure.Mapping;

public class InfrastructureMapping : Profile
{
    public InfrastructureMapping()
    {
        CreateMap<DogDto, Dog>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new DogId(Guid.Parse(src.Id))))
        .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => new OwnerId(Guid.Parse(src.OwnerId))));
        CreateMap<OwnerDto, Owner>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new OwnerId(Guid.Parse(src.Id))));
        CreateMap<ShowDto, Show>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ShowId(Guid.Parse(src.Id))));
        CreateMap<PlacementDto, Placement>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new PlacementId(Guid.Parse(src.Id))))
        .ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => new ShowId(Guid.Parse(src.ShowId))))
        .ForMember(dest => dest.DogId, opt => opt.MapFrom(src => new DogId(Guid.Parse(src.DogId))));
    }
}
