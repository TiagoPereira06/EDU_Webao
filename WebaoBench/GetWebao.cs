using DynWebao.IWebaos;
using Webao.Test.LastFmWebaos;
using Webao.Test.TvMazeWebaos;

namespace WebaoBench
{
    public static class GetWebao
    {
        public static object GetArtistInfo(WebaoArtist artist)
        {
            return artist.GetInfo("muse");
        }

        public static object GetDynArtistInfo(IWebaoDynArtist dynArtist)
        {
            return dynArtist.GetInfo("muse");
        }

        public static object GetTrackTop(WebaoTrack track)
        {
            return track.GeoGetTopTracks("australia");
        }

        public static object GetDynTrackTop(IWebaoDynTrack dynTrack)
        {
            return dynTrack.GeoGetTopTracks("australia");
        }

        public static object GetEpisodeInfo(WebaoEpisode episode)
        {
            return episode.Search(82, 6, 9);
        }

        public static object GetDynEpisodeInfo(IWebaoDynEpisode dynEpisode)
        {
            return dynEpisode.Search(82, 6, 9);
        }
    }
}