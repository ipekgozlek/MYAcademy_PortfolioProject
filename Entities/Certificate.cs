using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Certificate
    {
        public int CertificateId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Issuer is required.")]
        [StringLength(100)]
        public string Issuer { get; set; } = string.Empty;

        [StringLength(40)]
        public string? DateText { get; set; }          // "Sep 2026", "2025", "Oct 2026 (in progress)"

        [Url(ErrorMessage = "Please enter a valid URL starting with http:// or https://")]
        public string? CertificateUrl { get; set; }    // Optional link to the certificate

        public bool IsActive { get; set; }
    }
}
