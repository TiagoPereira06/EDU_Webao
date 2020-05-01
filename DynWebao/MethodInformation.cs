using System;
using System.Collections.Generic;
using System.Reflection;
using Webao.Attributes;

namespace DynWebao
{
    public class MethodInformation
    {
        public String Name { get; set; }
        public Type ReturnType { get; set; }
        public IList<Type> Args { get; set; }
        public MappingAttribute Mapping { get; set; }
        public GetAttribute Get { get; set; }
        public MethodAttributes MethodAttrs { get; set; }
    }
}