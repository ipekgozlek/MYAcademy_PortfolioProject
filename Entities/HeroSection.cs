namespace MYAcademy_PortfolioProject.Entities
{
    public class HeroSection
    {
        public int HeroSectionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string SubTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
