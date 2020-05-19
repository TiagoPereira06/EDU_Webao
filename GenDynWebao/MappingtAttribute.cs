using System;

namespace GenDynWebao
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MappingAttribute : Attribute
    {
        public readonly Type dto;

        public MappingAttribute(Type dto, string with)
        {
            With = with;
            this.dto = dto;
        }

        public MappingAttribute(Type dto)
        {
            this.dto = dto;
        }

        public string With { get; set; }
    }
}