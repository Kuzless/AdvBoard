using AdvBoard.Application.DTO;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementByUserIdQuery
{
    public class GetAnnouncementByUserIdQuery : IRequest<List<AdvDTO>>
    {
        public string UserId { get; set; }
    }
}
