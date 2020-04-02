using Webao.Attributes;
using Webao.Test.Dto.TvShows;

namespace Webao.Test
{
    [BaseUrl("http://api.tvmaze.com/")]
    [AddParameterAttribute("format", "json")]


    public class WebaoPerson : AbstractAccessObject
    {
        public WebaoPerson(IRequest req) : base(req)
        {
        }
        
        [Get("people/{id}")]
        [Mapping(typeof(Person), "")]
        public Person GetInfo(int personId)
        {
            return (Person) Request(personId);
        }
        
    }
}