using System;
using System.Linq;
using System.Reflection.Emit;

namespace DynWebao
{
    public class Emitter
    {
        public void EmitMethod(TypeBuilder typeBuilder,MethodInformation methodInfo)
        {
            BuilderHelper builderHelper = new BuilderHelper();
            Console.WriteLine(methodInfo.Args);
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(methodInfo.Name, methodInfo.MethodAttrs,
                methodInfo.ReturnType, methodInfo.Args.ToArray());
            
            ILGenerator ilGen = methodBuilder.GetILGenerator();
            ilGen.Emit(OpCodes.Ldstr, methodInfo.Mapping.path);
            ilGen.Emit(OpCodes.Stloc_0);
            //TODO : ITERAR OS ARGS PARA COMPLETE PATH(DÚVIDA)
            foreach (var arg in methodInfo.Args)
            {
                //ilGen.Emit();
            }
            
            ilGen.Emit(OpCodes.Ldc_I4,methodInfo.Args.Count);
            ilGen.Emit(OpCodes.Newarr);
            //DEPOIS DE TER O PATH
            ilGen.Emit(OpCodes.Ldtoken, methodInfo.Mapping.destType);
            //ilGen.Emit(OpCodes.Call,class[mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle);


        }

        public AssemblyBuilder Init()
        {
            throw new System.NotImplementedException();
        }
    }
}