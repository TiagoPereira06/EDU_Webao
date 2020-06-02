using System.Collections.Generic;
using Webao.Test.Dto.LastFm;

namespace GenDynWebao.IGenericWebaos
{
    public interface IWebaoGenericTrack
    {
        List<Track> GeoGetTopTracks(string country);
    }
}