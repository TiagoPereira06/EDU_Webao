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
        public TypeBuilder TypeBuilder;
        private ModuleBuilder moduleBuilder;
        private AssemblyBuilder asmBuilder;
        private String filename;
        public MethodInformation ProcessMethod(MethodInfo method)
        {
            MappingAttribute mappingAttribute = null;
            GetAttribute getAttribute = null;
            IList<Type> args = method.GetParameters().Select(arg => arg.ParameterType).ToList();
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
            MethodInformation info = new MethodInformation {Name = method.Name, ReturnType = method.ReturnType, Args = args};
            if (mappingAttribute != null) info.Mapping = mappingAttribute;
            if (getAttribute != null) info.Get = getAttribute;
            info.MethodAttrs = method.Attributes;
            return info;
        }

        public void SetTypeBuilder(Type interfaceType)
        {
            TypeBuilder = moduleBuilder.DefineType(interfaceType.Name, TypeAttributes.AnsiClass, typeof(Base));
        }

        public void SetModuleBuilder(string name)
        {
            AssemblyName asmName = new AssemblyName {Name = name};
            AppDomain appDom = Thread.GetDomain();
            asmBuilder = appDom.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave); 
            filename = asmName.Name + ".exe";
            moduleBuilder = asmBuilder.DefineDynamicModule(asmName.Name, filename);
        }

        public ConstructorInfo GetBaseCtor()
        {
            ConstructorInfo ctor = typeof(Base).GetTypeInfo().DeclaredConstructors.First();
            return ctor;
        }

        public void Save()
        {
            asmBuilder.Save(filename);
        }
    }
}