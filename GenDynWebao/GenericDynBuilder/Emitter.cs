using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using DynWebao;
using GenericWebao.GenericDynBuilder;
using Webao;
using Webao.Attributes;

namespace GenDynWebao.GenericDynBuilder
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

        public void EmitMethod(TypeBuilder typeBuilder, MethodInformation methodInfo, int delegateIdx)
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
                BindingFlags.Public | BindingFlags.Static));

            ilGen.Emit(OpCodes.Callvirt, typeof(IRequest).GetMethod("Get"));

            var returnType = methodInfo.ReturnType;
            if (delegateIdx != -1)
                GenResult(ilGen, methodInfo.Mapping, returnType, delegateIdx);
            else
                GenResult(ilGen, methodInfo.Mapping, returnType);

            ilGen.Emit(OpCodes.Ret);
        }

        private void GenResult(ILGenerator ilGen, MappingAttribute attribute, Type returnType, int index)
        {
            var resultDto = attribute.Dto;
            var valueType = resultDto.IsValueType;
            ilGen.DeclareLocal(resultDto);
            ilGen.Emit(valueType ? OpCodes.Unbox_Any : OpCodes.Castclass, attribute.Dto);
            ilGen.Emit(OpCodes.Stloc_1);
            ilGen.Emit(OpCodes.Ldsfld, typeof(MethodInformation).GetFields()[0]);
            ilGen.Emit(OpCodes.Ldc_I4_S, index);
            ilGen.Emit(OpCodes.Callvirt, typeof(List<Delegate>).GetMethod("get_Item"));
            ilGen.Emit(OpCodes.Ldc_I4_1);
            ilGen.Emit(OpCodes.Newarr, typeof(object));
            ilGen.Emit(OpCodes.Dup);
            ilGen.Emit(OpCodes.Ldc_I4_0);
            ilGen.Emit(OpCodes.Ldloc_1);
            if (valueType) ilGen.Emit(OpCodes.Box, resultDto);

            ilGen.Emit(OpCodes.Stelem_Ref);
            ilGen.Emit(OpCodes.Callvirt, typeof(Delegate).GetMethod("DynamicInvoke"));
            ilGen.Emit(OpCodes.Castclass, returnType);
        }

        private static void GenResult(ILGenerator ilGen, MappingAttribute mappingAttribute, Type returnType)
        {
            var resultDto = mappingAttribute.Dto;
            if (mappingAttribute.With != null) //MODE WITH
            {
                var resultWith = mappingAttribute.With;
                var funcType = typeof(Func<object>);
                ilGen.Emit(OpCodes.Ldftn, resultDto.GetMethod(resultWith));
                ilGen.Emit(OpCodes.Newobj, funcType.GetConstructors()[0]);
                ilGen.Emit(OpCodes.Callvirt, funcType.GetMethod("Invoke"));
                ilGen.Emit(OpCodes.Castclass, returnType);
            }
            else //MODE PATH
            {
                if (!resultDto.IsValueType) ilGen.Emit(OpCodes.Castclass, mappingAttribute.Dto);
                var propertyNames = mappingAttribute.Path.Split('.').Where(s => s.Length > 0).ToList();
                for (var i = 0; i < propertyNames.Count; ++i)
                {
                    var propertyName = propertyNames[i];
                    if (resultDto.IsValueType)
                    {
                        ilGen.DeclareLocal(resultDto);
                        if (i == 0) ilGen.Emit(OpCodes.Unbox_Any, resultDto);
                        ilGen.Emit(OpCodes.Stloc, i + 1);
                        ilGen.Emit(OpCodes.Ldloca_S, i + 1);
                    }

                    var aux = resultDto?.GetProperty(propertyName);
                    ilGen.Emit(OpCodes.Call, aux?.GetGetMethod());
                    resultDto = aux?.PropertyType;
                }
            }
        }
    }
}