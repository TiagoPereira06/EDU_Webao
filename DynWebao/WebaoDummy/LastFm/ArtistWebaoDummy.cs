using System.Collections.Generic;
using DynWebao.IWebaos;
using Webao;
using Webao.Test.Dto.LastFm;

namespace DynWebao.WebaoDummy.LastFm
{
    public class ArtistWebaoDummy : Base, IWebaoDynArtist
    {
        public ArtistWebaoDummy(IRequest req) : base(req)
        {
        }


        public Artist GetInfo(string name)
        {
            var path = "?method=artist.getinfo&artist={name}";
            var dto = (DtoArtist) req.Get(CompletePath(path, new[] {name}), typeof(DtoArtist));
            return dto.Artist;
        }

        public List<Artist> Search(string name, int page)
        {
            var path = "?method=artist.search&artist={name}&page={page}";
            var dto = (DtoSearch) req.Get(CompletePath(path, new[] {name, page.ToString()}), typeof(DtoSearch));
            return dto.Results.ArtistMatches.Artist;
        }
    }
}