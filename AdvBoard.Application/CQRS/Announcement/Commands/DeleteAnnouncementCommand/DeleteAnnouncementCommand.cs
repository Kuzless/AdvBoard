using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Commands.DeleteAnnouncementCommand
{
    public class DeleteAnnouncementCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
    }
}
