using System;
using System.Collections.Generic;
using DynWebao.IWebaos;
using Webao;
using Webao.Test.Dto.LastFm;

namespace DynWebao.WebaoDummy.LastFm
{
    public class TrackWebaoDummy : Base, IWebaoDynTrack
    {
        public TrackWebaoDummy(IRequest req) : base(req) {}

        public List<Track> GeoGetTopTracks(string country)
        {
            String path = "?method=geo.gettoptracks&country={country}";
            var dto = (DtoGeoTopTracks) req.Get(CompletePath(path, new[] {country.ToString()}), typeof(DtoGeoTopTracks));
            return dto.Tracks.Track;
        }
    }
}