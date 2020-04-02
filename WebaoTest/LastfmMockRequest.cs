using System;
using System.Collections.Generic;
using Webao.Test.Dto.LastFm;

namespace Webao
{
    public class LastfmMockRequest : IRequest
    {
        private Dictionary<Type,object> lastFmObjects = new Dictionary<Type, object>();
        
        public LastfmMockRequest()
        {
            DtoArtist dtoArtist = new DtoArtist();
            dtoArtist.Artist.Name = "Muse";
            dtoArtist.Artist.Mbid = "fd857293-5ab8-40de-b29e-55a69d4e4d0f";
            dtoArtist.Artist.Url = "https://www.last.fm/music/Muse";
            dtoArtist.Artist.Stats.Listeners = 4172632;
            dtoArtist.Artist.Stats.Playcount = 355245952;
            lastFmObjects.Add(typeof(DtoArtist),dtoArtist);

        }
        public IRequest BaseUrl(string host)
        {
            throw new NotImplementedException();
        }

        public IRequest AddParameter(string arg, string val)
        {
            throw new NotImplementedException();
        }

        public object Get(string path, Type targetType)
        {
            return lastFmObjects[targetType];
        }
    }
}