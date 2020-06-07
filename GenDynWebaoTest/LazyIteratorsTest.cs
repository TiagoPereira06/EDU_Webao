using System;
using GenDynWebao.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericWebaoTest
{
    [TestClass]
    public class LazyIteratorsTest
    {
        [TestMethod]
        public void TestLazyGetTopTracks()
        {
            var tracks = new ServiceTracks();
            var results = 0;
            foreach (var track in tracks.TopTracksFrom("portugal"))
            {
                ++results;
                Console.WriteLine(track.Name + " -By- " + track.Artist.Name);
                if (results != 86) continue;
                //LIMIT = 20 -> 86/20 = 5 REQUESTS
                Assert.AreEqual(tracks.GetCurrentPage(), 5);
                break;
            }
        }

        [TestMethod]
        public void TestLazySearchArtist()
        {
            var artists = new ServiceArtist();
            var results = 0;
            foreach (var artist in artists.SearchFor("Mallu"))
            {
                ++results;
                Console.WriteLine(artist.Name);
                if (results != 10) continue;
                //LIMIT = 2 -> 10/2 = 5 REQUESTS
                Assert.AreEqual(artists.GetCurrentPage(), 5);
                break;
            }
        }
    }
}