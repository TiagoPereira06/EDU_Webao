using System.Collections.Generic;
using Webao;
using Webao.Test.Dto.LastFm;
using Webao.Test.LastFmWebaos;

namespace GenDynWebao
{
    public class ServiceTracks
    {
        private readonly WebaoTrack webao;
        public ServiceTracks() : this(new HttpRequest()) {}

        private ServiceTracks(IRequest req)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Track> TopTracksFrom(string country)
        {
            throw new System.NotImplementedException();
        }
    }
}