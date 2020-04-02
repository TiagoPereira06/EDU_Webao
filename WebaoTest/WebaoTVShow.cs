using Webao.Attributes;
using Webao.Test.Dto.TvShows;

namespace Webao.Test
{
    [BaseUrl("https://www.episodate.com/api/")]
    [AddParameterAttribute("page", "1")]

    public class WebaoTvShow : AbstractAccessObject

    {
        public WebaoTvShow(IRequest req) : base(req)
        {
            
        }
        
        [Get("show-details?q={name}")]
        [Mapping(typeof(DtoTvShow), ".TvShow")]
        public TvShow GetInfo(string tvShowName)
        {
            return (TvShow) Request(tvShowName);
        }
        
        [Get("search?q={name}")]
        [Mapping(typeof(DtoTvShowSearch), ".Tv_Shows")]
        public TvShow[] Search(string tvShowName)
        {
            return (TvShow[]) Request(tvShowName);
        }
    }
}