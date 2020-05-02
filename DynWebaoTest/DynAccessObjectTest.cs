using System;
using DynWebao;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webao;

namespace DynWebaoTest
{
    [TestClass]
    public class DynAccessObjectTest
    {

        /*[TestMethod]
        public void TestWebaoDynArtistGetInfo()
        {
            var artist = artistWebao.GetInfo("muse");
            Assert.AreEqual("Muse", artist.Name);
            Assert.AreEqual("fd857293-5ab8-40de-b29e-55a69d4e4d0f", artist.Mbid);
            Assert.AreEqual("https://www.last.fm/music/Muse", artist.Url);
            Assert.AreNotEqual(0, artist.Stats.Listeners);
            Assert.AreNotEqual(0, artist.Stats.Playcount);
        }*/
        [TestMethod]
        public void Teste() {
            IRequest req = new HttpRequest();
            IWebaoDynArtist webaoDyn = 
                (IWebaoDynArtist) WebaoDynBuilder
                    .Build(typeof(IWebaoDynArtist), req);    
            /*ArtistWebaoDummy artistWebaoDummy = new ArtistWebaoDummy();
            var path = artistWebaoDummy.Search("muse", 1);
            Console.WriteLine(path);*/
        }
    }
}