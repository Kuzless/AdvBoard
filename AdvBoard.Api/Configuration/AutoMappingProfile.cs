using AdvBoard.Application.CQRS.Announcement.Commands.AddAnnouncementCommand;
using AdvBoard.Application.CQRS.Announcement.Commands.UpdateAnnouncementCommand;
using AdvBoard.Application.CQRS.User.Commands.SignUpCommand;
using AdvBoard.Application.DTO;
using AdvBoard.Domain.Entities;
using AutoMapper;

namespace AdvBoard.Api.Configuration
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<AuthDTO, SignUpCommand>();
            CreateMap<SignUpCommand, User>();
            
            CreateMap<AddAnnouncementCommand, Announcement>();
            CreateMap<EditAdvDTO, AddAnnouncementCommand>();
            CreateMap<UpdateAnnouncementCommand, Announcement>();
            CreateMap<EditAdvDTO, UpdateAnnouncementCommand>();
        }
    }
}
