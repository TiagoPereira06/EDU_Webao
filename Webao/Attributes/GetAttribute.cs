using System;

namespace Webao.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GetAttribute : Attribute
    {
        public readonly string path;

        public GetAttribute(string path)
        {
            this.path = path;
        }
    }
}