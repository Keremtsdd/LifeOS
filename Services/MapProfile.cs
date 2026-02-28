using AutoMapper;
using LifeOs.DTOs;
using LifeOs.Entities;

namespace LifeOs.Mappings
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {

            CreateMap<ActivityCreateDto, UserActivity>();

            CreateMap<UserActivity, ActivityDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<Category, CategoryDto>();

            CreateMap<Category, CategoryProgressDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ColorHex, opt => opt.MapFrom(src => src.ColorHex))
                .ForMember(dest => dest.Score, opt => opt.Ignore())
                .ForMember(dest => dest.Percentage, opt => opt.Ignore());

            CreateMap<User, UserProfileDto>();
        }
    }
}