using AdvBoard.Application.DTO.QueryDTOs;
using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Queries.GetAnnouncementStructureQuery
{
    public class GetAnnouncementStructureQuery : IRequest<AdvStructureDTO>{}
}
