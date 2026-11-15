using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Education
    {
        public int EducationId { get; set; }
        [Required(ErrorMessage = "School is required.")]
        public string Title { get; set; } = string.Empty;
        public string? SubTitle { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Date range is required.")]
        public string Duration { get; set; } = string.Empty;
        public string? Location { get; set; }    
        public bool IsActive { get; set; }

    }
}
