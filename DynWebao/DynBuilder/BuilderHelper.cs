﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Webao.Attributes;

namespace DynWebao.DynBuilder
{
    public class BuilderHelper
    {
        private AssemblyBuilder asmBuilder;
        private string filename;
        private ModuleBuilder moduleBuilder;
        public TypeBuilder TypeBuilder;

        public MethodInformation ProcessMethod(MethodInfo method)
        {
            MappingAttribute mappingAttribute = null;
            GetAttribute getAttribute = null;
            IList<Type> args = method.GetParameters().Select(arg => arg.ParameterType).ToList();
            foreach (var attr in method.GetCustomAttributes<Attribute>())
                switch (attr)
                {
                    case MappingAttribute ma:
                        mappingAttribute = ma;
                        break;
                    case GetAttribute ga:
                        getAttribute = ga;
                        break;
                }

            var info = new MethodInformation {Name = method.Name, ReturnType = method.ReturnType, Args = args};
            if (mappingAttribute != null) info.Mapping = mappingAttribute;
            if (getAttribute != null) info.Get = getAttribute;
            info.MethodAttrs = method.Attributes;
            return info;
        }

        public void SetTypeBuilder(Type interfaceType)
        {
            TypeBuilder = moduleBuilder.DefineType(interfaceType.Name.Remove(0, 1),
                TypeAttributes.AnsiClass | TypeAttributes.Public
                                         | TypeAttributes.AutoClass, typeof(Base));
            TypeBuilder.AddInterfaceImplementation(interfaceType);
        }

        public void SetModuleBuilder(string name)
        {
            var asmName = new AssemblyName {Name = name};
            var appDom = Thread.GetDomain();
            asmBuilder = appDom.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.RunAndSave);
            filename = asmName.Name + ".dll";
            moduleBuilder = asmBuilder.DefineDynamicModule(asmName.Name, filename);
        }

        public ConstructorInfo GetBaseCtor()
        {
            return typeof(Base).GetTypeInfo().DeclaredConstructors.First();
        }

        public void Save()
        {
            asmBuilder.Save(filename);
        }
    }
}