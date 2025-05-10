using AdvBoard.Application.DTO;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementsQuery
{
    public class GetAnnouncementsQuery : IRequest<List<AdvDTO>>{}
}
