using System.Collections.Generic;
using DynWebao.IWebaos;
using Webao;
using Webao.Test.Dto.LastFm;

namespace DynWebao.WebaoDummy.LastFm
{
    public class TrackWebaoDummy : Base, IWebaoDynTrack
    {
        public TrackWebaoDummy(IRequest req) : base(req)
        {
        }

        public List<Track> GeoGetTopTracks(string country)
        {
            var path = "?method=geo.gettoptracks&country={country}";
            var dto = (DtoGeoTopTracks) req.Get(CompletePath(path, new[] {country}), typeof(DtoGeoTopTracks));
            return dto.Tracks.Track;
        }
    }
}