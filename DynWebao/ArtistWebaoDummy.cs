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
        public ArtistWebaoDummy(IRequest req)
        {
            this.req = req;
        }

        public Artist GetInfo(object name)
        {
            String path = "?method=artist.getinfo&artist={name}";
            DtoArtist dto = (DtoArtist) req.Get(CompletePath(path,new[] {name.ToString()}), typeof(DtoArtist));
            return dto.Artist;
        }

        public List<Artist> Search(object name, object page)
        {
            String path = "?method=artist.search&artist={name}&page={page}";
            DtoSearch dto = (DtoSearch) req.Get(CompletePath(path,new []{name.ToString(),page.ToString()}), typeof(DtoSearch));
            return dto.Results.ArtistMatches.Artist;
        }
        
    }
}