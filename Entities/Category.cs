using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; }

        public List<Project>? Projects { get; set; }
    }
}