using System;

namespace Webao.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MappingAttribute : Attribute
    {
        public readonly Type destType;
        public readonly string path;

        public MappingAttribute(Type dest, string path)
        {
            this.path = path;
            destType = dest;
        }
    }
}