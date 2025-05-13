using AdvBoard.Application.CQRS.Announcement.Commands.AddAnnouncementCommand;
using AdvBoard.Application.CQRS.Announcement.Commands.UpdateAnnouncementCommand;
using AdvBoard.Application.CQRS.User.Commands;
using AdvBoard.Application.DTO.CommandDTOs;
using AdvBoard.Application.DTO.QueryDTOs;
using AdvBoard.Domain.Entities;
using AutoMapper;

namespace AdvBoard.Api.Configuration
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            // user commands
            CreateMap<UserDTO, AuthorizeUserGenerateTokenCommand>();

            // announcement commands
            CreateMap<AddAnnouncementCommand, Announcement>();
            CreateMap<UpdAdvDTO, AddAnnouncementCommand>();
            CreateMap<UpdateAnnouncementCommand, Announcement>();
            CreateMap<UpdAdvDTO, UpdateAnnouncementCommand>();

            // announcements
            CreateMap<Announcement, AdvDTO>()
                .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.SubCategory.Name))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.SubCategory.Category.Name))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.Name))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.User.Name))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("dd/MM/yyyy HH:mm:ss")));
            CreateMap<Announcement, EditAdvInfoDTO>()
                .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.SubCategory.Id))
                .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.SubCategory.Category.Id))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.Status.Id));

            // categories
            CreateMap<Category, CategoryDTO>();

            // subcategories
            CreateMap<SubCategory, SubCategoryDTO>();

            // statuses
            CreateMap<Status, StatusDTO>();
        }
    }
}
