using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        [Required(ErrorMessage = "Position is required.")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Company is required.")]
        public string CompanyName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;
        public string? SideTitle { get; set; }
        [Required(ErrorMessage = "Date range is required.")]
        public string Duration { get; set; } = string.Empty;
        [Required(ErrorMessage = "Work type is required.")]
        public string WorkType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
