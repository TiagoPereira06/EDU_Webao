using System.Collections.Generic;
using Webao;
using Webao.Base;
using Webao.Test.Dto.LastFm;
using Webao.Test.LastFmWebaos;

namespace GenDynWebao.Services
{
    public class ServiceTracks
    {
        private readonly WebaoTrack webao;
        private int page = 1;

        public ServiceTracks() : this(new LazyHttpRequest())
        {
        }

        private ServiceTracks(IRequest req)
        {
            req.BaseUrl("http://ws.audioscrobbler.com/2.0/");
            req.AddParameter("format", "json");
            req.AddParameter("api_key", LastFmAPI.API_KEY);
            webao = new WebaoTrack(req);
        }

        public IEnumerable<Track> TopTracksFrom(string country)
        {
            webao.req.SetPage(page.ToString());
            const int limit = 20;
            webao.req.SetLimit(limit.ToString());
            var result = webao.GeoGetTopTracks(country);
            for (var i = 0; i < result.Count; ++i)
            {
                if ((i + 1) % limit == 0 && i != 0)
                {
                    page++;
                    webao.req.SetPage(page.ToString());
                    result = webao.GeoGetTopTracks(country);
                    if (result.Count == limit) i = 0;
                }

                yield return result[i];
            }
        }

        public int GetCurrentPage()
        {
            return page;
        }
    }
}