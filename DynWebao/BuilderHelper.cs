using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Webao.Attributes;

namespace DynWebao
{
    public class BuilderHelper
    {
        public static MethodInformation ProcessMethod(MethodInfo method)
        {
            MappingAttribute mappingAttribute = null;
            GetAttribute getAttribute = null;
            String methodName = method.Name;
            IList<Type> args = method.GetParameters().Select(arg => arg.ParameterType).ToList();
            Type returnType = method.ReturnType;
            foreach (var attr in method.GetCustomAttributes<Attribute>())
            {
                switch (attr)
                {
                    case MappingAttribute ma:
                        mappingAttribute = ma;
                        break;
                    case GetAttribute ga:
                        getAttribute = ga;
                        break;
                }
            }

            MethodInformation info = new MethodInformation {Name = methodName, ReturnType = returnType, Args = args};
            if (mappingAttribute != null) info.Mapping = mappingAttribute;
            if (getAttribute != null) info.Get = getAttribute;
            info.MethodAttrs = method.Attributes;
            return info;
        }

        public static TypeBuilder GetTypeBuilder(Type interfaceType, ModuleBuilder moduleBuilder)
        {
            return moduleBuilder.DefineType(interfaceType.Name, interfaceType.Attributes, typeof(Base));
        }

        public ModuleBuilder getModuleBuilder(string name)
        {
            AssemblyName asmName = new AssemblyName {Name = name};
            AppDomain appDom = Thread.GetDomain();
            AssemblyBuilder asmBuilder = appDom.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
            string filename = asmName.Name + ".exe";
            return asmBuilder.DefineDynamicModule(asmName.Name, filename);
        }
    }

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