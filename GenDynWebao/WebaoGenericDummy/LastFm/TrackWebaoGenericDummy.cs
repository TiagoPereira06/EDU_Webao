using System.Collections.Generic;
using DynWebao;
using GenDynWebao.IGenericWebaos;
using GenericWebao.GenericDynBuilder;
using Webao;
using Webao.Test.Dto.LastFm;

namespace GenDynWebao.WebaoGenericDummy.LastFm
{
    public class TrackWebaoGenericDummy : Base, IWebaoGenericTrack
    {
        public TrackWebaoGenericDummy(IRequest req) : base(req)
        {
        }

        public List<Track> GeoGetTopTracks(string country)
        {
            var path = "?method=geo.gettoptracks&country={country}";
            var dto = (DtoGeoTopTracks) req.Get(CompletePath(path, new[] {country}), typeof(DtoGeoTopTracks));
            return (List<Track>) MethodInformation.Delegates[0].DynamicInvoke(dto);
        }
    }
}