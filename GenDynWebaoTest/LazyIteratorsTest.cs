using GenDynWebao;
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
            var counter = 0;
            foreach (var track in tracks.TopTracksFrom("portugal"))
            {
                ++counter;
                if (counter != 86) continue;
                //LIMIT = 20 -> 86/20 = 5
                Assert.AreEqual(tracks.GetCurrentPage(), 5);
                break;
            }
        }
    }
}