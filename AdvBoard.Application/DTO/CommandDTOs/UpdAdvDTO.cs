using System.Text.Json.Serialization;

namespace AdvBoard.Application.DTO.CommandDTOs
{
    public class UpdAdvDTO
    {
        [JsonPropertyName("id")]
        public int? Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("subCategoryId")]
        public int SubCategoryId { get; set; }
        [JsonPropertyName("statusId")]
        public int? StatusId { get; set; }
    }
}
