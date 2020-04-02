using Webao.Attributes;
using Webao.Base;
using Webao.Test.Dto.OMDb;

namespace Webao.Test
{
    [BaseUrl("http://www.omdbapi.com/")]
    [AddParameter("r", "json")]
    [AddParameter("apikey", OpenMovieDataBaseAPI.API_KEY)]
    public class WebaoMovie : AbstractAccessObject
    {
        public WebaoMovie(IRequest req) : base(req)
        {
        }

        [Get("?t={name}")]
        [Mapping(typeof(Movie), "")]
        public Movie GetInfo(string name)
        {
            return (Movie) Request(name);
        }

        [Get("?s={name}")]
        [Mapping(typeof(DtoMovieSearch), ".Search")]
        public Movie[] Search(string name)
        {
            return (Movie[]) Request(name);
        }
    }
}