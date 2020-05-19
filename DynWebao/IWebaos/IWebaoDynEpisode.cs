using Webao.Attributes;
using Webao.Test.Dto.TvMaze;

namespace DynWebao.IWebaos
{
    [BaseUrl("http://api.tvmaze.com/")]
    [AddParameter("format", "json")]
    public interface IWebaoDynEpisode
    {
        [Get("shows/{tvShowId}/episodebynumber?season={epSeason}&number={epNumber}")]
        [Mapping(typeof(Episode), "")]
        Episode Search(int tvShowId, int episodeSeason, int episodeNumber);
    }
}