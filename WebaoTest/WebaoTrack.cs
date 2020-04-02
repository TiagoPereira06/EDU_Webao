﻿using System.Collections.Generic;
using Webao.Attributes;
using Webao.Base;
using Webao.Test.Dto.LastFm;

namespace Webao.Test
{
    [BaseUrl("http://ws.audioscrobbler.com/2.0/")]
    [AddParameterAttribute("format", "json")]
    [AddParameterAttribute("api_key", LastFmAPI.API_KEY)]
    public class WebaoTrack : AbstractAccessObject
    {
        public WebaoTrack(IRequest req) : base(req)
        {
        }

        [Get("?method=geo.gettoptracks&country={country}")]
        [Mapping(typeof(DtoGeoTopTracks), ".Tracks.Track")]
        public List<Track> GeoGetTopTracks(string country)
        {
            return (List<Track>) Request(country);
        }
    }
}