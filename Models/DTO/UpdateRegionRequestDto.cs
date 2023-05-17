namespace first_web_api.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        public string Code { get; set; }

        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}