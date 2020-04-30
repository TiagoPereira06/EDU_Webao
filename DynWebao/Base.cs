using System.Collections.Generic;
using System.Linq;

namespace DynWebao
{
    public abstract class Base
    {
        protected static string CompletePath(string path, IEnumerable<object> fill)
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