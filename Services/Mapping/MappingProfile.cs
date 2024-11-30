using AutoMapper;
using Data.Models;
using Services.DTO;


namespace Services.Mapping
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mapeo de UserDTO a User
            CreateMap<GroupsFamilyDto, GroupsFamily>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameGroupFamily));
            CreateMap<GroupsFamily, GroupsFamilyDto>().ForMember(dest => dest.NameGroupFamily, opt => opt.MapFrom(src => src.Name));

            CreateMap<UserCreateDto, User>();
            CreateMap<User, UserCreateDto>();

            CreateMap<GiftDto, Gift>();
            CreateMap<Gift, GiftDto>();
            CreateMap<GiftOperationDto, Gift>();
            CreateMap<Gift, GiftOperationDto>();

            CreateMap<GroupsFamilyOperationDto, GroupsFamily>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameGroupFamily));
            CreateMap<GroupsFamily, GroupsFamilyOperationDto>().ForMember(dest => dest.NameGroupFamily, opt => opt.MapFrom(src => src.Name));

            CreateMap<RoundOperationDto, Round>().ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description));
            CreateMap<Round, RoundOperationDto>().ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description));
            CreateMap<RoundDto, Round>().ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.description));
            CreateMap<Round, RoundDto>().ForMember(dest => dest.description, opt => opt.MapFrom(src => src.Description));

            CreateMap<UserRoundDto, UserRound>().ReverseMap();

            CreateMap<AssigmentsDto, Assignment>().ReverseMap();

            CreateMap<UserDto, User>();
            // Si necesitas el mapeo inverso:
            CreateMap<User, UserDto>();
        }
    }
}
