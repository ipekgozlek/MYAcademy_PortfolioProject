using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class HeroSection
    {
        public int HeroSectionId { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Job title is required.")]
        public string SubTitle { get; set; } = string.Empty;
        [Required(ErrorMessage = "Intro text is required.")]
        public string Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
    }
}
