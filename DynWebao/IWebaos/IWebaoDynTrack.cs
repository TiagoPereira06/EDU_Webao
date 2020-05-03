using System.Collections.Generic;
using Webao.Attributes;
using Webao.Base;
using Webao.Test.Dto.LastFm;

namespace DynWebao.IWebaos
{
    [BaseUrl("http://ws.audioscrobbler.com/2.0/")]
    [AddParameter("format", "json")]
    [AddParameter("api_key", LastFmAPI.API_KEY)]
    public interface IWebaoDynTrack
    {
        [Get("?method=geo.gettoptracks&country={country}")]
        [Mapping(typeof(DtoGeoTopTracks), ".Tracks.Track")]
        List<Track> GeoGetTopTracks(string country);
    }
}