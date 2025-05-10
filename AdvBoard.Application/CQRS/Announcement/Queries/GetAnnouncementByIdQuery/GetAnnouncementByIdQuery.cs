using AdvBoard.Application.DTO;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementByIdQuery
{
    public class GetAnnouncementByIdQuery : IRequest<AdvDTO>
    {
        public int Id { get; set; }
    }
}
