using AdvBoard.Application.DTO.QueryDTOs;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementByIdQuery
{
    public class GetAnnouncementEditByIdQuery : IRequest<EditAdvInfoDTO>
    {
        public int Id { get; set; }
    }
}
