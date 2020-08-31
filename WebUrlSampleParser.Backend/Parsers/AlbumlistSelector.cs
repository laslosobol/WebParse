using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using SampleProject.Backend.Model;

namespace WebUrlSampleParser.Backend.Parsers
{
    public class AlbumlistSelector : AbstractSelector
    {
        private static IConfiguration _config = Configuration.Default.WithDefaultLoader();
        private static IBrowsingContext _context = BrowsingContext.New(_config);
        static string _albumNameSelector = "td.main_entry a.list_album";
        static string _artistNameSelector = "td.main_entry a.list_artist";
        static string _releaseDateSelector = "td.main_entry span.rel_date";
        private static string _listTitleSelector = "li.currentverylong";
        private static string _listImgSelector = "a img";
        public new static async Task<List<Album>> Select(string address)
        {
            List<Album> albumsChart = new List<Album>();
            var document = await _context.OpenAsync(address);
            var artists = document.QuerySelectorAll(_artistNameSelector);
            var names = document.QuerySelectorAll(_albumNameSelector);
            var releaseDate = document.QuerySelectorAll(_releaseDateSelector);
            var listTitle = document.QuerySelector(_listTitleSelector);
            var listImg = document.QuerySelectorAll(_listImgSelector).Where(el => el.GetAttribute("title") == "Panopticon")
                .Select(el => el.GetAttribute("src"));
            var img = "https:" + listImg.First();
            foreach (var album in artists.Zip(names.Zip(releaseDate,
                (x, y) => (x, y)), (x, y) => (x, y)))
            {
                Album albumResult = new Album();
                albumResult.Title = album.y.x.TextContent;
                albumResult.Artist = album.x.TextContent;
                albumResult.ReleaseDate = album.y.y.TextContent;
                albumResult.CustomList = listTitle.TextContent;
                albumResult.ListImg = img;
                albumsChart.Add(albumResult);
            }
            return albumsChart;
        }
    }
}