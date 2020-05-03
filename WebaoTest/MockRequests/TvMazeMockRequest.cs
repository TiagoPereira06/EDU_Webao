using System;
using System.Collections.Generic;
using Webao.Test.Dto.TvMaze;

namespace Webao.Test.MockRequests
{
    public class TvMazeMockRequest : IRequest
    {
        private readonly Dictionary<Type,object> tvMazeObjects = new Dictionary<Type, object>();

        public TvMazeMockRequest()
        {
            InitMockObjects();
        }

        private void InitMockObjects()
        {
            TvShow tvShow = new TvShow {Name = "Game of Thrones", Premiered = "2011-04-17", Language = "English"};
            Person person = new Person {Name = "Emilia Clarke", Url = "http://www.tvmaze.com/people/14079/emilia-clarke", Gender = "Female"};
            Episode episode = new Episode
                {Id = 82, Name = "Battle of the Bastards", Number = 9, Runtime = 90, Season = 6, Url = ""};
            tvMazeObjects.Add(typeof(TvShow),tvShow);
            tvMazeObjects.Add(typeof(Person),person);
            tvMazeObjects.Add(typeof(Episode),episode);
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
            return tvMazeObjects[targetType];

        }
    }
}