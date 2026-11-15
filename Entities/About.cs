namespace MYAcademy_PortfolioProject.Entities
{
    public class About
    {
        public int AboutId { get; set; }
        public string NameSurname { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Expert { get; set; }
        public string? Email { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? OpenToWork { get; set; }
    }
}
