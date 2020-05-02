using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Webao;

namespace DynWebao
{
    public class Emitter
    {
        public void EmitConstructor(TypeBuilder typeBuilder, ConstructorInfo baseCtor)
        {
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public |
                MethodAttributes.SpecialName |
                MethodAttributes.RTSpecialName,
                CallingConventions.Standard,
                new[] {typeof(IRequest)});

            ILGenerator ilGen = constructorBuilder.GetILGenerator();
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Call, baseCtor);
            ilGen.Emit(OpCodes.Ldarg_0);
            ilGen.Emit(OpCodes.Ldarg_1);
            //TODO : ST do Campo req passado no construtor - >
            ilGen.Emit(OpCodes.Stfld,typeof(IRequest));
            
            ilGen.Emit(OpCodes.Ret);
        }

        public void EmitMethod(TypeBuilder typeBuilder, MethodInformation methodInfo)
        {
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, MethodAttributes.Family|MethodAttributes.Virtual|
                                                                                    MethodAttributes.Public,
                methodInfo.ReturnType, methodInfo.Args.ToArray());

            ILGenerator ilGen = methodBuilder.GetILGenerator();
            ilGen.DeclareLocal(typeof(string));
            ilGen.Emit(OpCodes.Ldstr, methodInfo.Get.path);
            ilGen.Emit(OpCodes.Stloc_0);
            ilGen.Emit(OpCodes.Ldarg_0);
            /* ldfld class [Webao]Webao.IRequest DynWebao.ArtistWebaoDummy::req
             * TODO: LDFLD do req - >
             */
            ilGen.Emit(OpCodes.Ldfld, typeof(IRequest));
            
            ilGen.Emit(OpCodes.Ldloc_0);
            ilGen.Emit(OpCodes.Ldc_I4, methodInfo.Args.Count);
            ilGen.Emit(OpCodes.Newarr, typeof(string));
            ilGen.Emit(OpCodes.Dup);
            ilGen.Emit(OpCodes.Ldc_I4_0);
            for (int i = 0; i < methodInfo.Args.Count; i++)
            {
                ilGen.Emit(OpCodes.Ldarg, i + 1);
                /* callvirt instance string [mscorlib]System.Object::ToString()
                 * TODO : CALL TO STRING ->
                 */
                ilGen.Emit(OpCodes.Callvirt, typeof(object).GetMethod("ToString",Type.EmptyTypes));
                ilGen.Emit(OpCodes.Stelem_Ref);
                if (i != methodInfo.Args.Count)
                    ilGen.Emit(OpCodes.Dup);
            } 
            ilGen.Emit(OpCodes.Call, typeof(Base).GetRuntimeMethods().ElementAt(0));
            ilGen.Emit(OpCodes.Ldtoken, methodInfo.Mapping.destType);
            ilGen.Emit(OpCodes.Callvirt, typeof(Type).GetMethod("GetTypeFromHandle",
                BindingFlags.Public | BindingFlags.Static) ?? throw new Exception());
            ilGen.Emit(OpCodes.Castclass, methodInfo.Mapping.destType);
            /* IL_0035:  callvirt   instance class [WebaoTest]Webao.Test.Dto.LastFm.Artist [WebaoTest]Webao.Test.Dto.LastFm.DtoArtist::get_Artist()
             * TODO : CALLVIRT get->
             */
            ilGen.Emit(OpCodes.Ldloc_0);
            ilGen.Emit(OpCodes.Ret);

        }
    }
}