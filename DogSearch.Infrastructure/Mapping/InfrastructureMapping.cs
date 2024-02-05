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
        SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
        DestinationMemberNamingConvention = new PascalCaseNamingConvention();
        CreateMap<DogDto, Dog>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new DogId(Guid.Parse(src.id))))
        .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => new OwnerId(Guid.Parse(src.owner_id))));
        CreateMap<OwnerDto, Owner>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new OwnerId(Guid.Parse(src.id))));
        CreateMap<ShowDto, Show>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new ShowId(Guid.Parse(src.id))));
        CreateMap<PlacementDto, Placement>()
        .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new PlacementId(Guid.Parse(src.id))))
        .ForMember(dest => dest.ShowId, opt => opt.MapFrom(src => new ShowId(Guid.Parse(src.show_id))))
        .ForMember(dest => dest.DogId, opt => opt.MapFrom(src => new DogId(Guid.Parse(src.dog_id))));
    }
}
