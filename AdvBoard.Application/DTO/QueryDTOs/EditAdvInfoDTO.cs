namespace AdvBoard.Application.DTO.QueryDTOs
{
    public class EditAdvInfoDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<SubCategoryDTO> SubCategories { get; set; }
        public List<StatusDTO> Statuses { get; set; }
    }
}
