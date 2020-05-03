using System;
using System.Collections.Generic;
using DynWebao.IWebaos;
using Webao;
using Webao.Test.Dto.LastFm;

namespace DynWebao.WebaoDummy.LastFm
{
    public class ArtistWebaoDummy : Base, IWebaoDynArtist
    {
        public ArtistWebaoDummy(IRequest req) : base(req){}


        public Artist GetInfo(string name)
        {
            String path = "?method=artist.getinfo&artist={name}";
            DtoArtist dto = (DtoArtist) req.Get(CompletePath(path,new[] {name.ToString()}), typeof(DtoArtist));
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