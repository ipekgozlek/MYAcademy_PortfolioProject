using System.ComponentModel.DataAnnotations;

namespace MYAcademy_PortfolioProject.Entities
{
    public class Message
    {
        public int MessageId { get; set; }

        [Required(ErrorMessage = "Ad soyad zorunludur.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Ad soyad 2-50 karakter olmalıdır.")]
        public string NameSurname { get; set; }

        [Required(ErrorMessage = "E-posta zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Konu zorunludur.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Konu 3-100 karakter olmalıdır.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mesaj zorunludur.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Mesaj en az 10 karakter olmalıdır.")]
        public string MessageDetail { get; set; }

        public DateTime SendDate { get; set; }

        public bool IsRead { get; set; }
    }
}
