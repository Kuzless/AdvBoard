namespace AdvBoard.MVC.Models.Requests
{
    public class AnnouncementAddRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int SubCategoryId { get; set; }
    }
}
