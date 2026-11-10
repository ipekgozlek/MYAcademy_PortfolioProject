using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class SkillCategory
    {
        public int SkillCategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(60)]
        public string Title { get; set; }            // "Backend Development"

        [StringLength(200)]
        public string? Description { get; set; }     // Kartın altındaki açıklama

        [StringLength(40)]
        public string? Icon { get; set; }            // "bi-hdd-network"

        public List<Skill> Skills { get; set; } = new();   // Bu kategorinin yetenekleri
    }
}