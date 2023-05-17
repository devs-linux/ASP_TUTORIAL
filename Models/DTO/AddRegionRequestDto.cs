namespace first_web_api.Models.DTO
{
    public class AddRegionRequestDto
    {
        public string Code { get; set; }

        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}