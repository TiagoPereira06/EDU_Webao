using System.Linq;
using Webao;

namespace DynWebao
{
    public abstract class Base
    {
        public IRequest req;

        protected Base(IRequest req)
        {
            this.req = req;
        }

        protected static string CompletePath(string path, string[] fill)
        {
            return fill.Aggregate(path, (current, arg) => FillPath(current, arg.ToString()));
        }

        private static string FillPath(string path, string fill)
        {
            var l = path.IndexOf('{');
            var r = path.IndexOf('}') + 1;
            return path.Replace(path.Substring(l, r - l), fill);
        }
    }
}