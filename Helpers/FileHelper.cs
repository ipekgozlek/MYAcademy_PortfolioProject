namespace MYAcademy_PortfolioProject.Helpers
{
    public static class FileHelper
    {
        // Dosyayı wwwroot/uploads/{folder} içine kaydeder.
        // Başarılıysa (yol, null), hatalıysa (null, hata mesajı) döner.
        public static (string? Path, string? Error) Save(
            IFormFile file, string webRootPath, string folder,
            string[] allowedExtensions, long maxBytes)
        {
            if (file == null || file.Length == 0)
                return (null, "Please select a file.");

            // 1) Boyut kontrolü
            if (file.Length > maxBytes)
                return (null, $"File is too large. Maximum size is {maxBytes / 1024 / 1024} MB.");

            // 2) Tür (uzantı) kontrolü
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(ext))
                return (null, "Allowed file types: " + string.Join(", ", allowedExtensions));

            // 3) Benzersiz dosya adı
            var fileName = $"{Guid.NewGuid()}{ext}";

            // 4) wwwroot/uploads/{folder} altına kaydet
            var directory = Path.Combine(webRootPath, "uploads", folder);
            Directory.CreateDirectory(directory);   // klasör yoksa oluşturur

            using (var stream = new FileStream(Path.Combine(directory, fileName), FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return ($"/uploads/{folder}/{fileName}", null);
        }

        // Sadece uploads klasöründeki dosyaları siler
        public static void Delete(string? relativePath, string webRootPath)
        {
            if (string.IsNullOrWhiteSpace(relativePath) || !relativePath.StartsWith("/uploads/"))
                return;

            var uploadsRoot = Path.GetFullPath(Path.Combine(webRootPath, "uploads"));
            var fullPath = Path.GetFullPath(Path.Combine(webRootPath, relativePath.TrimStart('/')));

            // "../" gibi hilelerle uploads dışına çıkılmasın
            if (!fullPath.StartsWith(uploadsRoot)) return;

            if (File.Exists(fullPath)) File.Delete(fullPath);
        }
        // Dosyanın gerçekten PDF olup olmadığını içeriğinden anlar
        public static bool IsRealPdf(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var header = new byte[4];
            stream.Read(header, 0, 4);
            return System.Text.Encoding.ASCII.GetString(header) == "%PDF";
        }
    }
}