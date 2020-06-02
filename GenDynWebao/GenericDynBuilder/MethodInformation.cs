using System;
using System.Collections.Generic;
using System.Reflection;
using Webao.Attributes;

namespace GenericWebao.GenericDynBuilder
{
    public class MethodInformation
    {
        public static List<Delegate> Delegates = new List<Delegate>();

        public static int Index = 0;
        public string Name { get; set; }
        public Type ReturnType { get; set; }
        public IList<Type> Args { get; set; }
        public MappingAttribute Mapping { get; set; }
        public GetAttribute Get { get; set; }
        public MethodAttributes MethodAttrs { get; set; }
    }
}