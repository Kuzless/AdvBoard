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
            CreateMap<UserDTO, SignUpCommand>();
            CreateMap<SignUpCommand, User>();
        }
    }
}
