using System.Text.RegularExpressions;

namespace MYAcademy_PortfolioProject.Helpers
{
    public static class DurationHelper
    {
        // "2024 – Günümüz" → 2024
        // "Ocak 2025 - Devam Ediyor" → 2025
        public static int StartYear(string duration)
        {
            var match = Regex.Match(duration ?? "", @"\d{4}");
            return match.Success ? int.Parse(match.Value) : 0;
        }
    }
}