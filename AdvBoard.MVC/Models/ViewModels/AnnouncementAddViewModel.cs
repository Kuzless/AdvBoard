namespace AdvBoard.MVC.Models.ViewModels
{
    public class AnnouncementAddViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public int? SubCategoryId { get; set; }
        public List<CategoryModel> Categories { get; set; }
        public List<SubCategoryModel> SubCategories { get; set; }
    }
}
