using System;
using System.Collections.Generic;
using Webao.Test.Dto.LastFm;

namespace Webao
{
    public class LastfmMockRequest : IRequest
    {
        private Dictionary<Type,object> lastFmObjects = new Dictionary<Type, object>();
        
        public LastfmMockRequest()
        {
            InitMockObjects();
        }

        private void InitMockObjects()
        {
            lastFmObjects.Add(typeof(DtoArtist), DtoArtistInit());
            lastFmObjects.Add(typeof(DtoSearch), DtoSearchInit());
            lastFmObjects.Add(typeof(DtoGeoTopTracks),DtoGeoTopTracksInit());
        }

        private static DtoGeoTopTracks DtoGeoTopTracksInit()
        {
            DtoGeoTopTracks dtoGeoTopTracks = new DtoGeoTopTracks {Tracks = new DtoTracks {Track = new List<Track>()}};
            Track track0 = new Track {Name = "The Less I Know the Better"};
            Track track1 = new Track {Name = "Mr. Brightside", Artist = new Artist {Name = "The Killers"}};
            dtoGeoTopTracks.Tracks.Track.Add(track0);
            dtoGeoTopTracks.Tracks.Track.Add(track1);
            return dtoGeoTopTracks;
        }

        private static DtoArtist DtoArtistInit()
        {
            DtoArtist dtoArtist = new DtoArtist
            {
                Artist = new Artist
                {
                    Name = "Muse",
                    Mbid = "fd857293-5ab8-40de-b29e-55a69d4e4d0f",
                    Url = "https://www.last.fm/music/Muse",
                    Stats = new Statistics {Listeners = 4172632, Playcount = 355245952}
                }
            };
            return dtoArtist;
        }

        private static DtoSearch DtoSearchInit()
        {
            DtoSearch dtoSearch = new DtoSearch
            {
                Results = new DtoResults {ArtistMatches = new DtoArtistMatches {Artist = new List<Artist>()}}
            };
            Artist artist1 = new Artist {Name = "Black Sabbath"};
            Artist artist2 = new Artist {Name = "Black Eyed Peas"};
            dtoSearch.Results.ArtistMatches.Artist.Add(new Artist());
            dtoSearch.Results.ArtistMatches.Artist.Add(artist1);
            dtoSearch.Results.ArtistMatches.Artist.Add(artist2);
            return dtoSearch;
        }

        public IRequest BaseUrl(string host)
        {
            return null;
        }

        public IRequest AddParameter(string arg, string val)
        {
            return null;
        }

        public object Get(string path, Type targetType)
        {
            return lastFmObjects[targetType];
        }
    }
}