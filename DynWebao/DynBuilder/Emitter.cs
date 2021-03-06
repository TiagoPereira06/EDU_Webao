﻿using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Webao;
using Webao.Attributes;

namespace DynWebao.DynBuilder
{
    public class Emitter
    {
        public void EmitConstructor(TypeBuilder typeBuilder, ConstructorInfo baseCtor)
        {
            var constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName,
                CallingConventions.HasThis,
                new[] {typeof(IRequest)});

            var ilGen = constructorBuilder.GetILGenerator();
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Ldarg_1);
            ilGen.Emit(OpCodes.Call, baseCtor);
            ilGen.Emit(OpCodes.Ret);
        }

        public void EmitMethod(TypeBuilder typeBuilder, MethodInformation methodInfo)
        {
            var methodBuilder = typeBuilder.DefineMethod(methodInfo.Name,
                MethodAttributes.Virtual | MethodAttributes.Public |
                MethodAttributes.ReuseSlot,
                methodInfo.ReturnType, methodInfo.Args.ToArray());


            var ilGen = methodBuilder.GetILGenerator();
            ilGen.DeclareLocal(typeof(string));
            ilGen.Emit(OpCodes.Ldstr, methodInfo.Get.path);
            ilGen.Emit(OpCodes.Stloc_0);
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Ldfld, typeof(Base).GetField("req"));
            ilGen.Emit(OpCodes.Ldloc_0);
            ilGen.Emit(OpCodes.Ldc_I4, methodInfo.Args.Count);
            ilGen.Emit(OpCodes.Newarr, typeof(string));
            ilGen.Emit(OpCodes.Dup);
            var argsCount = methodInfo.Args.Count;
            for (var i = 0; i < argsCount; i++)
            {
                ilGen.Emit(OpCodes.Ldc_I4, i);
                var index = i + 1;
                if (methodInfo.Args[i].IsValueType) //INT
                {
                    ilGen.Emit(OpCodes.Ldarga_S, index);
                    ilGen.Emit(OpCodes.Call, typeof(int).GetMethod("ToString", Type.EmptyTypes));
                }
                else //STRING
                {
                    ilGen.Emit(OpCodes.Ldarg, index);
                    ilGen.Emit(OpCodes.Callvirt, typeof(object).GetMethod("ToString", Type.EmptyTypes));
                }

                ilGen.Emit(OpCodes.Stelem_Ref);
                if (i != argsCount - 1 && argsCount > 1)
                    ilGen.Emit(OpCodes.Dup);
            }

            ilGen.Emit(OpCodes.Call, typeof(Base).GetRuntimeMethods().ElementAt(0));
            ilGen.Emit(OpCodes.Ldtoken, methodInfo.Mapping.Dto);
            ilGen.Emit(OpCodes.Call, typeof(Type).GetMethod("GetTypeFromHandle",
                BindingFlags.Public | BindingFlags.Static) ?? throw new Exception());
            ilGen.Emit(OpCodes.Callvirt, typeof(IRequest).GetMethod("Get"));
            GenResult(ilGen, methodInfo.Mapping);
            ilGen.Emit(OpCodes.Ret);
        }

        private static void GenResult(ILGenerator ilGen, MappingAttribute mappingAttribute)
        {
            var property = mappingAttribute.Dto;
            if (!property.IsValueType) ilGen.Emit(OpCodes.Castclass, mappingAttribute.Dto);
            var propertyNames = mappingAttribute.Path.Split('.').Where(s => !s.Equals("")).ToArray();
            for (var i = 0; i < propertyNames.Length; ++i)
            {
                var propertyName = propertyNames[i];
                if (property.IsValueType)
                {
                    ilGen.DeclareLocal(property);
                    if (i == 0) ilGen.Emit(OpCodes.Unbox_Any, property);
                    ilGen.Emit(OpCodes.Stloc, i + 1);
                    ilGen.Emit(OpCodes.Ldloca_S, i + 1);
                }

                var aux = property?.GetProperty(propertyName);
                ilGen.Emit(OpCodes.Call, aux?.GetGetMethod());
                property = aux?.PropertyType;
            }
        }
    }
}