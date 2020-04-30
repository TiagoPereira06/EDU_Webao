using System.Collections.Generic;
using Webao.Attributes;
using Webao.Base;
using Webao.Test.Dto.LastFm;

namespace Webao.Test.LastFmWebaos
{
    [BaseUrl("http://ws.audioscrobbler.com/2.0/")]
    [AddParameter("format", "json")]
    [AddParameter("api_key", LastFmAPI.API_KEY)]
    public class WebaoArtist : AbstractAccessObject
    {
        public WebaoArtist(IRequest req) : base(req)
        {
        }

        [Get("?method=artist.getinfo&artist={name}")]
        [Mapping(typeof(DtoArtist), ".Artist")]
        public Dto.LastFm.Artist GetInfo(string name)
        {
            return (Dto.LastFm.Artist) Request(name);
        }


        [Get("?method=artist.search&artist={name}")]
        [Mapping(typeof(DtoSearch), ".Results.ArtistMatches.Artist")]
        public List<Dto.LastFm.Artist> Search(string name)
        {
            return (List<Dto.LastFm.Artist>) Request(name);
        }
    }
}