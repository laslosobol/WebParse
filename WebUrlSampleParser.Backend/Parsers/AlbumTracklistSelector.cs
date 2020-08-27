using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using SampleProject.Backend.Model;

namespace WebUrlSampleParser.Backend.Parsers
{
    public class AlbumTracklistSelector : AbstractSelector
    {
        private static IConfiguration _config = Configuration.Default.WithDefaultLoader();
        private static IBrowsingContext _context = BrowsingContext.New(_config);
        private static string _cellNameSelector = "ul#tracks a.affiliate_link";
        private static string _cellLengthSelector = "ul#tracks span.tracklist_duration";
        private static string _albumNameSelector = "a.sametitle";
        private static string _artistNameSelector = "a.artist";
        private static string _imgSelector = "img.coverart_img";
        public new static async Task<List<Track>> Select(string address)
        {
            List<Track> tracks = new List<Track>();
            var document = await _context.OpenAsync(address);
            var cells = document.QuerySelectorAll(_cellNameSelector);
            var length = document.QuerySelectorAll(_cellLengthSelector);
            var artist = document.QuerySelector(_artistNameSelector);
            var album = document.QuerySelector(_albumNameSelector);
            var img = document.QuerySelectorAll(_imgSelector).Select(el => el.GetAttribute("src").ToString());
            foreach (var tuple in cells.Zip(length, (x, y) => (x, y)))
            {
                Track track = new Track();
                track.Artist = artist.TextContent;
                track.Album = album.TextContent;
                track.Duration = tuple.y.TextContent;
                track.Title = tuple.x.TextContent;
                track.ImageLink = "https:" + img.First();
                tracks.Add(track);
            }
            return tracks;
        }
    }
}