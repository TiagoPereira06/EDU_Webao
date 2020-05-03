using DynWebao.DynBuilder;
using DynWebao.IWebaos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webao;

namespace DynWebaoTest
{
    [TestClass]
    public class DynAccessObjectTest
    {
        
        private static readonly IWebaoDynArtist dynArtistWebao = 
            (IWebaoDynArtist) WebaoDynBuilder
                .Build(typeof(IWebaoDynArtist), new HttpRequest());
        
        private static readonly IWebaoDynTrack dynTrackWebao = 
            (IWebaoDynTrack) WebaoDynBuilder
                .Build(typeof(IWebaoDynTrack), new HttpRequest());
        
        private static readonly IWebaoDynEpisode dynEpisodeWebao = 
            (IWebaoDynEpisode) WebaoDynBuilder
                .Build(typeof(IWebaoDynEpisode), new HttpRequest());

        [TestMethod]
        public void TestWebaoDynArtistGetInfo() {
            var artist = dynArtistWebao.GetInfo("muse");
            Assert.AreEqual("Muse", artist.Name);
            Assert.AreEqual("fd857293-5ab8-40de-b29e-55a69d4e4d0f", artist.Mbid);
            Assert.AreEqual("https://www.last.fm/music/Muse", artist.Url);
            Assert.AreNotEqual(0, artist.Stats.Listeners);
            Assert.AreNotEqual(0, artist.Stats.Playcount);
        }
        
        [TestMethod]
        public void TestWebaoDynArtistSearch()
        {
            var artists = dynArtistWebao.Search("black",1);
            Assert.AreEqual("Black Sabbath", artists[1].Name);
            Assert.AreEqual("Black Eyed Peas", artists[2].Name);
        }
        
        [TestMethod]
        public void TestWebaoDynTrackGeoGetTopTracks()
        {
            var tracks = dynTrackWebao.GeoGetTopTracks("australia");
            Assert.AreEqual("The Less I Know the Better", tracks[0].Name);
            Assert.AreEqual("Mr. Brightside", tracks[1].Name);
            Assert.AreEqual("The Killers", tracks[1].Artist.Name);
        }
        
        [TestMethod]
        public void TestWebaoDynEpisodeSearch()
        {
            var episode = dynEpisodeWebao.Search(82, 6, 9);
            Assert.AreEqual("Battle of the Bastards", episode.Name);
            Assert.AreEqual(6, episode.Season);
            Assert.AreNotEqual(25, episode.Number);
            Assert.AreNotEqual(200, episode.Runtime);
        }
    }
}