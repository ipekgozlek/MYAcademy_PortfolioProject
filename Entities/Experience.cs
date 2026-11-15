namespace MYAcademy_PortfolioProject.Entities
{
    public class Experience
    {
        public int ExperienceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? SideTitle { get; set; }
        public string Duration { get; set; } = string.Empty;
        public string WorkType { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
