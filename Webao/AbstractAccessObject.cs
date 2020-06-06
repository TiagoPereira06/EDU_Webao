using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Webao.Attributes;

namespace Webao
{
    public abstract class AbstractAccessObject
    {
        public readonly IRequest req;

        protected AbstractAccessObject(IRequest req)
        {
            this.req = req;
        }

        public object Request(params object[] args)
        {
            var stackTrace = new StackTrace();
            var callSite = (MethodInfo) stackTrace.GetFrame(1).GetMethod();
            /*
             * The callsite is the caller method.
             */
            var getAttribute = callSite.GetCustomAttribute<GetAttribute>(false);
            var mappingAttribute = callSite.GetCustomAttribute<MappingAttribute>(false);
            var path = CompletePath(getAttribute.path, args);
            var reply = req.Get(path, mappingAttribute.Dto);
            var graphIndex = reply;
            var graphSchema = mappingAttribute.Path.Split('.').Where(s => !s.Equals("")).ToArray();
            foreach (var propertyName in graphSchema)
                graphIndex = graphIndex?.GetType().GetProperty(propertyName)?.GetValue(graphIndex);
            return graphIndex;
        }

        private static string CompletePath(string path, IEnumerable<object> fill)
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