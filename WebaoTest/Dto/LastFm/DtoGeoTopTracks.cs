using System.Collections.Generic;

namespace Webao.Test.Dto.LastFm
{
    public struct DtoGeoTopTracks
    {
        public DtoTracks Tracks { get; set; }
    }

    public struct DtoTracks
    {
        public List<Track> Track { get; set; }
    }
}