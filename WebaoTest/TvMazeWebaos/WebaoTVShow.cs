using Webao.Attributes;
using Webao.Test.Dto.TvMaze;

namespace Webao.Test.TvMazeWebaos
{
    [BaseUrl("http://api.tvmaze.com/")]
    [AddParameter("format", "json")]
    public class WebaoTvShow : AbstractAccessObject

    {
        public WebaoTvShow(IRequest req) : base(req)
        {
        }

        [Get("shows/{tvShowId}")]
        [Mapping(typeof(TvShow), "")]
        public TvShow GetInfo(int tvShowId)
        {
            return (TvShow) Request(tvShowId);
        }
    }
}