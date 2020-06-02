using System;

namespace Webao.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MappingAttribute : Attribute
    {
        public MappingAttribute(Type dto, string path)
        {
            Dto = dto;
            Path = path;
        }

        public MappingAttribute(Type dto)
        {
            Dto = dto;
        }

        public Type Dto { get; }
        public string Path { get; }

        public string With { get; set; }
    }
}