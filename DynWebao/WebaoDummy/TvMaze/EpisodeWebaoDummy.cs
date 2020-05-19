using DynWebao.IWebaos;
using Webao;
using Webao.Test.Dto.TvMaze;

namespace DynWebao.WebaoDummy.TvMaze
{
    public class EpisodeWebaoDummy : Base, IWebaoDynEpisode
    {
        public EpisodeWebaoDummy(IRequest req) : base(req)
        {
        }

        public Episode Search(int tvShowId, int episodeSeason, int episodeNumber)
        {
            var path = "shows/{tvShowId}/episodebynumber?season={epSeason}&number={epNumber}";
            return (Episode) req.Get(CompletePath(path,
                new[] {tvShowId.ToString(), episodeSeason.ToString(), episodeNumber.ToString()}), typeof(Episode));
        }
    }
}