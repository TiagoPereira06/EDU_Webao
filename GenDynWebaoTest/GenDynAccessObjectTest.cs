using DynWebao.IWebaos;
using GenDynWebao.IGenericWebaos;
using GenericWebao.GenericDynBuilder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webao;
using Webao.Base;
using Webao.Test.Dto.LastFm;

namespace GenericWebaoTest
{
    [TestClass]
    public class GenDynAccessObjectTest
    {
        private static readonly IWebaoGenericArtist new_genDynArtistWebao = (IWebaoGenericArtist) WebaoGenericBuilder
            .For<IWebaoGenericArtist>("http://ws.audioscrobbler.com/2.0/")
            .AddParameter("format", "json")
            .AddParameter("api_key", LastFmAPI.API_KEY)
            .On("GetInfo")
            .GetFrom("?method=artist.getinfo&artist={name}")
            .Mapping<DtoArtist>(dto => dto.Artist)
            .On("Search")
            .GetFrom("?method=artist.search&artist={name}&page={page}")
            .Mapping<DtoSearch>(dto => dto.Results.ArtistMatches.Artist)
            .Build(new HttpRequest());

        private static readonly IWebaoGenericTrack newGenericTrackWebao = (IWebaoGenericTrack) WebaoGenericBuilder
            .For<IWebaoGenericTrack>("http://ws.audioscrobbler.com/2.0/")
            .AddParameter("format", "json")
            .AddParameter("api_key", LastFmAPI.API_KEY)
            .On("GeoGetTopTracks")
            .GetFrom("?method=geo.gettoptracks&country={country}")
            .Mapping<DtoGeoTopTracks>(dto => dto.Tracks.Track)
            .Build(new HttpRequest());

        private static readonly IWebaoDynArtist dynArtistWebao =
            (IWebaoDynArtist) WebaoGenericBuilder
                .Build(typeof(IWebaoDynArtist), new HttpRequest());

        private static readonly IWebaoDynTrack dynTrackWebao =
            (IWebaoDynTrack) WebaoGenericBuilder
                .Build(typeof(IWebaoDynTrack), new HttpRequest());

        public GenDynAccessObjectTest()
        {
            MethodInformation.Index = 0;
        }

        [TestMethod]
        public void Old_TestWebaoDynArtistGetInfo()
        {
            var artist = dynArtistWebao.GetInfo("muse");
            Assert.AreEqual("Muse", artist.Name);
            Assert.AreEqual("fd857293-5ab8-40de-b29e-55a69d4e4d0f", artist.Mbid);
            Assert.AreEqual("https://www.last.fm/music/Muse", artist.Url);
            Assert.AreNotEqual(0, artist.Stats.Listeners);
            Assert.AreNotEqual(0, artist.Stats.Playcount);
        }

        [TestMethod]
        public void Old_TestWebaoDynArtistSearch()
        {
            var artists = dynArtistWebao.Search("black", 1);
            Assert.AreEqual("Black Sabbath", artists[1].Name);
            Assert.AreEqual("Black Eyed Peas", artists[2].Name);
        }

        [TestMethod]
        public void Old_TestWebaoDynTrackGeoGetTopTracks()
        {
            var tracks = dynTrackWebao.GeoGetTopTracks("australia");
            Assert.AreEqual("The Less I Know the Better", tracks[0].Name);
            Assert.AreEqual("Mr. Brightside", tracks[1].Name);
            Assert.AreEqual("The Killers", tracks[1].Artist.Name);
        }

        [TestMethod]
        public void New_TestWebaoGenDynArtistGetInfo()
        {
            var artist = new_genDynArtistWebao.GetInfo("muse");
            Assert.AreEqual("Muse", artist.Name);
            Assert.AreEqual("fd857293-5ab8-40de-b29e-55a69d4e4d0f", artist.Mbid);
            Assert.AreEqual("https://www.last.fm/music/Muse", artist.Url);
            Assert.AreNotEqual(0, artist.Stats.Listeners);
            Assert.AreNotEqual(0, artist.Stats.Playcount);
        }

        [TestMethod]
        public void New_TestWebaoGenDynArtistSearch()
        {
            var artists = new_genDynArtistWebao.Search("black", 1);
            Assert.AreEqual("Black Sabbath", artists[1].Name);
            Assert.AreEqual("Black Eyed Peas", artists[2].Name);
        }

        [TestMethod]
        public void New_TestWebaoGenDynTrackGeoGetTopTracks()
        {
            var tracks = newGenericTrackWebao.GeoGetTopTracks("australia");
            Assert.AreEqual("The Less I Know the Better", tracks[0].Name);
            Assert.AreEqual("Mr. Brightside", tracks[1].Name);
            Assert.AreEqual("The Killers", tracks[1].Artist.Name);
        }
    }
}