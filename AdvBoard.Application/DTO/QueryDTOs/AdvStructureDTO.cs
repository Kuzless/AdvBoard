namespace AdvBoard.Application.DTO.QueryDTOs
{
    public class AdvStructureDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public List<SubCategoryDTO> SubCategories { get; set; }
    }
}
