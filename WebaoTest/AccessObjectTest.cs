﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Webao.Test
{
    [TestClass]
    public class AccessObjectTest
    {
        private static readonly WebaoArtist artistWebao =
            (WebaoArtist) WebaoBuilder.Build(typeof(WebaoArtist), new HttpRequest());

        private static readonly WebaoTrack trackWebao =
            (WebaoTrack) WebaoBuilder.Build(typeof(WebaoTrack), new HttpRequest());


        [TestMethod]
        public void TestWebaoArtistGetInfo()
        {
            var artist = artistWebao.GetInfo("muse");
            Assert.AreEqual("Muse", artist.Name);
            Assert.AreEqual("fd857293-5ab8-40de-b29e-55a69d4e4d0f", artist.Mbid);
            Assert.AreEqual("https://www.last.fm/music/Muse", artist.Url);
            Assert.AreNotEqual(0, artist.Stats.Listeners);
            Assert.AreNotEqual(0, artist.Stats.Playcount);
        }

        [TestMethod]
        public void TestWebaoArtistSearch()
        {
            var artists = artistWebao.Search("black");
            Assert.AreEqual("Black Sabbath", artists[1].Name);
            Assert.AreEqual("Black Eyed Peas", artists[2].Name);
        }

        [TestMethod]
        public void TestWebaoTrackGeoGetTopTracks()
        {
            var tracks = trackWebao.GeoGetTopTracks("australia");
            Assert.AreEqual("The Less I Know the Better", tracks[0].Name);
            Assert.AreEqual("Mr. Brightside", tracks[1].Name);
            Assert.AreEqual("The Killers", tracks[1].Artist.Name);
        }
    }

    [TestClass]
    public class AccessMovieObjectTest
    {
        private static readonly WebaoMovie movieWebao =
            (WebaoMovie) WebaoBuilder.Build(typeof(WebaoMovie), new HttpRequest());

        [TestMethod]
        public void TestWebaoMovieGetInfo()
        {
            var movie = movieWebao.GetInfo("Saint George");
            Assert.AreEqual("tt4895668", movie.imdbID);
            Assert.AreNotEqual(2020, movie.Year);
        }

        [TestMethod]
        public void TestWebaoMovieSearch()
        {
            var moviesResult = movieWebao.Search("The Godfather");
            Assert.AreEqual(1972, moviesResult[0].Year);
            Assert.AreEqual(1974, moviesResult[1].Year);
            Assert.AreEqual(1990, moviesResult[2].Year);
        }
    }

    [TestClass]
    public class AccessTvShowObjectTest
    {
        private static readonly WebaoTvShow tvShowWebao =
            (WebaoTvShow) WebaoBuilder.Build(typeof(WebaoTvShow), new HttpRequest());
        
        [TestMethod]
        public void TestWebaoTvShowGetInfo()
        {
            var tvShow = tvShowWebao.GetInfo("Homeland");
            Assert.AreEqual("US", tvShow.Country);
            Assert.AreEqual(26708, tvShow.Id);
            Assert.AreNotEqual("Netflix", tvShow.Network);
        }

        [TestMethod]
        public void TestWebaoTvShowSearch()
        {
            var tvShowsResult = tvShowWebao.Search("Life");
            Assert.AreEqual("18 to Life", tvShowsResult[0].Name);
            Assert.AreEqual("24 to Life", tvShowsResult[1].Name);
            Assert.AreEqual("A Chef's Life", tvShowsResult[2].Name);
        }
    }

}