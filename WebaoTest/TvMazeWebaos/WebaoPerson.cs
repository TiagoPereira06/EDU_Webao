using Webao.Attributes;
using Webao.Test.Dto.TvMaze;

namespace Webao.Test.TvMazeWebaos
{
    [BaseUrl("http://api.tvmaze.com/")]
    [AddParameter("format", "json")]
    public class WebaoPerson : AbstractAccessObject
    {
        public WebaoPerson(IRequest req) : base(req)
        {
        }

        [Get("people/{personId}")]
        [Mapping(typeof(Person), "")]
        public Person GetInfo(int personId)
        {
            return (Person) Request(personId);
        }
    }
}