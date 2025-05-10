namespace AdvBoard.Domain.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Announcement> Announcements { get; set; }
    }
}
