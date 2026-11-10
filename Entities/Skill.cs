using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Skill
    {
        public int SkillId { get; set; }

        [Required(ErrorMessage = "Skill name is required.")]
        [StringLength(50)]
        public string Title { get; set; }            // "C#"

        [Required]
        [StringLength(20)]
        public string Level { get; set; }            // "İleri" / "Orta" / "Başlangıç"

        public int SkillCategoryId { get; set; }     // Hangi kategoriye ait (foreign key)
        public SkillCategory? SkillCategory { get; set; }
    }
}