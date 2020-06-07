using System.Collections.Generic;
using Webao;
using Webao.Base;
using Webao.Test.Dto.LastFm;
using Webao.Test.LastFmWebaos;

namespace GenDynWebao.Services
{
    public class ServiceArtist
    {
        private readonly WebaoArtist webao;
        private int page = 1;

        public ServiceArtist() : this(new LazyHttpRequest())
        {
        }

        private ServiceArtist(IRequest req)
        {
            req.BaseUrl("http://ws.audioscrobbler.com/2.0/");
            req.AddParameter("format", "json");
            req.AddParameter("api_key", LastFmAPI.API_KEY);
            webao = new WebaoArtist(req);
        }

        public IEnumerable<Artist> SearchFor(string artistName)
        {
            webao.req.SetPage(page.ToString());
            const int limit = 3;
            webao.req.SetLimit(limit.ToString());
            var result = webao.Search(artistName);
            for (var i = 0; i < result.Count; ++i)
            {
                if ((i + 1) % limit == 0 && i != 0)
                {
                    page++;
                    webao.req.SetPage(page.ToString());
                    result = webao.Search(artistName);
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