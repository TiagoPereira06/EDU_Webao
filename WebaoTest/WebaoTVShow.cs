using Webao.Attributes;
using Webao.Test.Dto.TvShows;

namespace Webao.Test
{
    [BaseUrl("http://api.tvmaze.com/")]
    [AddParameterAttribute("format", "json")]

    public class WebaoTvShow : AbstractAccessObject

    {
        public WebaoTvShow(IRequest req) : base(req)
        {
            
        }
        
        [Get("shows/{name}")]
        [Mapping(typeof(TvShow), "")]
        public TvShow GetInfo(int tvShowId)
        {
            return (TvShow) Request(tvShowId);
        }
        
    }
}