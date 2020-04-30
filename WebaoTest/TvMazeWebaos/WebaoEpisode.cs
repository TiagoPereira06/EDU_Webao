using Webao.Attributes;
using Webao.Test.Dto.TvMaze;

namespace Webao.Test.TvMazeWebaos
{
    [BaseUrl("http://api.tvmaze.com/")]
    [AddParameter("format", "json")]
    
    
    public class WebaoEpisode : AbstractAccessObject
    {
        public WebaoEpisode(IRequest req) : base(req)
        {
        }

        [Get("shows/{tvShowId}/episodebynumber?season={epSeason}&number={epNumber}")]
        [Mapping(typeof(Episode), "")]
        public Episode 
            Search(int tvShowId, int episodeSeason, int episodeNumber)
        {
            return (Episode) Request(tvShowId, episodeSeason, episodeNumber);
        }
    }
}