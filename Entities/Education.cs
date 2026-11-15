namespace MYAcademy_PortfolioProject.Entities
{
    public class Education
    {
        public int EducationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? SubTitle { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string? Location { get; set; }    
        public bool IsActive { get; set; }

    }
}
