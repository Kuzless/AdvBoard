using MediatR;

namespace AdvBoard.Application.CQRS.Announcement.Commands.UpdateAnnouncementCommand
{
    public class UpdateAnnouncementCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public int StatusId { get; set; }
        public string UserId { get; set; }
    }
}
