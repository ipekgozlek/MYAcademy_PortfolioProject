using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Contact
    {
        public int ContactId { get; set; }
        public string? Description { get; set; }
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string? Email { get; set; }
        public string? Linkedin { get; set; }
        public string? Location { get; set; }
        public string? Github { get; set; }
        public string? SubDescription { get; set; }
        public string? CvFilePath { get; set; }
    }
}
