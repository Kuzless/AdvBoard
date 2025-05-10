using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Commands.AddAnnouncementCommand
{
    public class AddAnnouncementCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public string UserId { get; set; }
    }
}
