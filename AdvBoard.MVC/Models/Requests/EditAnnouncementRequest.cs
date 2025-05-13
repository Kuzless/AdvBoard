namespace AdvBoard.MVC.Models.Requests
{
    public class EditAnnouncementRequest
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
        public int? StatusId { get; set; }
    }
}
