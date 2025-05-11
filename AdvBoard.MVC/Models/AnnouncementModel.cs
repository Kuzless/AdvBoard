namespace AdvBoard.MVC.Models
{
    public class AnnouncementModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedAt { get; set; }
        public string Status { get; set; }
        public string Owner { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
    }
}
