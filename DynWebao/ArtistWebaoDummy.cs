using System;
using System.Collections.Generic;
using Webao;
using Webao.Base;
using Webao.Test.Dto.LastFm;

namespace DynWebao
{
    public class ArtistWebaoDummy : Base, IWebaoDynArtist
    {
        private readonly IRequest req;
        public ArtistWebaoDummy()
        {
            req = new HttpRequest();
            req.BaseUrl("http://ws.audioscrobbler.com/2.0/");
            req.AddParameter("format", "json");
            req.AddParameter("api_key", LastFmAPI.API_KEY);
            
        }

        public Artist GetInfo(string name)
        {
            String path = "?method=artist.getinfo&artist={name}";
            DtoArtist dto = (DtoArtist) req.Get(CompletePath(path,new object[] {name.ToString()}), typeof(DtoArtist));
            return dto.Artist;
        }

        public List<Artist> Search(string name, int page)
        {
            String path = "?method=artist.search&artist={name}&page={page}";
            DtoSearch dto = (DtoSearch) req.Get(CompletePath(path,new []{name.ToString(),page.ToString()}), typeof(DtoSearch));
            return dto.Results.ArtistMatches.Artist;
        }
        
    }
}