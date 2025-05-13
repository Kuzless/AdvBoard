using AdvBoard.Application.DTO.QueryDTOs;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementsQuery
{
    public class GetAnnouncementsQuery : IRequest<List<AdvDTO>>{}
}
