using System.Text.RegularExpressions;

namespace DotNetCoreSqlDb.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsValidDate(string? Date)    
        {
            if (Date == null) return true;
            var regex = new Regex(@"^(voor|na|tussen|circa)?\s*((\d{1,2})-(\d{1,2})-(\d{4})|(\d{1,2})-(\d{4})|(\d{4}))(?:(?:\s*en\s*(\d{1,2})-(\d{1,2})-(\d{4})|(\d{1,2})-(\d{4}))|(\s*en\s*\d{4}))?$");
            return regex.IsMatch(Date);
        }
    }
}