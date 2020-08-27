using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using SampleProject.Backend.Model;

namespace WebUrlSampleParser.Backend.Parsers
{
    public class ChartAlbumlistSelector : AbstractSelector
    {
        private static IConfiguration _config = Configuration.Default.WithDefaultLoader();
        private static IBrowsingContext _context = BrowsingContext.New(_config);
        private static string _artistNameSelector = "table.mbgen span.chart_detail_line1";
        private static string _albumNameSelector = "table.mbgen a.album";
        private static string _releaseDateSelector = "table.mbgen div.chart_year";
        //private static string __pictureSelector = "img.delayloading";

        public new static async Task<List<Album>> Select(string address)
        {
            List<Album> albumsChart = new List<Album>();
            var document = await _context.OpenAsync(address);
            var artists = document.QuerySelectorAll(_artistNameSelector);
            var names = document.QuerySelectorAll(_albumNameSelector);
            var releaseDate = document.QuerySelectorAll(_releaseDateSelector);
            //var pictures = document.QuerySelectorAll(__pictureSelector).Select(el => el.GetAttribute("data-delayloadurl").ToString());
            //Console.WriteLine(artists.Length);
            //Console.WriteLine(names.Length);
            //Console.WriteLine(releaseDate.Length);
            foreach (var album in artists.Zip(names.Zip(releaseDate,
                (x, y) => (x, y)), (x, y) => (x, y)))
            {
                Album albumResult = new Album();
                albumResult.Title = album.y.x.TextContent;
                albumResult.Artist = album.x.TextContent;
                albumResult.ReleaseDate = album.y.y.TextContent;
                //albumResult.ImageLink = album.y.y;
                albumsChart.Add(albumResult);
                
            }
            return albumsChart;
        }
    }
}