using AutoMapper;
using PresentPractic.Models.DbModels;
using PresentPractic.Models.DTOModels;

namespace PresentPractic.Services;

public class MapperService : Profile
{

    public MapperService()
    {
        CreateMap<User, DTOUser>()
            .ForMember(to_ => to_.Id , o => o.MapFrom(src => src.Id) )
            .ForMember(to_ => to_.Login , o => o.MapFrom(src => src.Login) )
            .ForMember(to_ => to_.Presents , o => o.MapFrom(src => src.Presents));


        CreateMap<Present, DTOPresent>()
            .ForMember(to_ => to_.Id, o => o.MapFrom(str => str.Id))
            .ForMember(to_ => to_.Name, o => o.MapFrom(src => src.PresentName))
            .ForMember(to_ => to_.Description, o => o.MapFrom(src => src.PresentDescription))
            .ForMember(to_ => to_.Status, o => o.MapFrom(src => src.Status));
    }
}