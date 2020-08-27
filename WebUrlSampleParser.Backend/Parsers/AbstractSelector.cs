using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace WebUrlSampleParser.Backend.Parsers
{
    public abstract class AbstractSelector
    {
        private static IConfiguration _config;
        private static IBrowsingContext _context;
        private static string _cellSelector;
        public static async Task<IHtmlCollection<IElement>> Select(string address)
        {
            var document = await _context.OpenAsync(address);
            var cells = document.QuerySelectorAll(_cellSelector);
            return cells;
        }
    }
}