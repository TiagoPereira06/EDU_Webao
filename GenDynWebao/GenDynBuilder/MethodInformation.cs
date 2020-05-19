using System;
using System.Collections.Generic;
using System.Reflection;
using Webao.Attributes;

namespace GenDynWebao.GenDynBuilder
{
    public class MethodInformation
    {
        public string Name { get; set; }
        public Type ReturnType { get; set; }
        public IList<Type> Args { get; set; }
        public MappingAttribute Mapping { get; set; }
        public GetAttribute Get { get; set; }
        public MethodAttributes MethodAttrs { get; set; }
    }
}