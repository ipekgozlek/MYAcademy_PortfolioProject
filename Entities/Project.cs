using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public string? GithubUrl { get; set; }
        public string? LiveUrl { get; set; }          // Canlı demo linki
        public string? CoverImageUrl { get; set; }
        public string? Technologies { get; set; }    

        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }         
        public Category? Category { get; set; }
    }
}