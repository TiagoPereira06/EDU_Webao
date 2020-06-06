using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using Webao;

namespace GenDynWebao
{
    public class LazyHttpRequest : IRequest
    {
        private static readonly HttpClient client = new HttpClient();

        private readonly Dictionary<string, string> queryParameters = new Dictionary<string, string>();
        private string host;

        public IRequest BaseUrl(string host)
        {
            this.host = host;
            return this;
        }

        public IRequest AddParameter(string arg, string val)
        {
            queryParameters.Add(arg, val);
            return this;
        }

        public object Get(string path, Type targetType)
        {
            /*
             * You should avoid blocking IO such as waiting for Result completion.
             * More about this topic on Concurrent Programming course!
             * For now we will keep it like this, although...
             */
            var body = client
                .GetStringAsync(Url(path))
                .Result;
            return JsonConvert.DeserializeObject(body, targetType);
        }

        public IRequest SetPage(string pageNumber)
        {
            if (queryParameters.ContainsKey("page"))
                queryParameters["page"] = pageNumber;
            else
                queryParameters.Add("page", pageNumber);
            return this;
        }

        public IRequest SetLimit(string limit)
        {
            if (queryParameters.ContainsKey("limit"))
                queryParameters["limit"] = limit;
            else
                queryParameters.Add("limit", limit);
            return this;
        }

        private string Url(string path)
        {
            var url = host + path;
            if (queryParameters.Count != 0 && !url.Contains("?"))
                url += "?";
            else
                url += "&";
            return queryParameters.Aggregate(url, (current, pair) => current + pair.Key + "=" + pair.Value + "&");
        }
    }
}