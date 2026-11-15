using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class About
    {
        public int AboutId { get; set; }
        [Required(ErrorMessage = "Full name is required.")]
        public string NameSurname { get; set; } = string.Empty;
        [Required(ErrorMessage = "Job title is required.")]
        public string JobTitle { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Expert { get; set; }
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Biography is required.")]
        public string Description { get; set; } = string.Empty;
        public string? OpenToWork { get; set; }
    }
}
