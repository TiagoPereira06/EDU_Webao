using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;

namespace Webao
{
    public class HttpRequest : IRequest
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

        public string Url(string path)
        {
            var url = host + path;
            if (queryParameters.Count != 0 && !url.Contains("?"))
                url += "?";
            else
                url += "&";
            foreach (var pair in queryParameters) url += pair.Key + "=" + pair.Value + "&";
            return url;
        }
    }
}