using System;
using System.Collections.Generic;
using System.Net.Configuration;
using System.Reflection.Emit;
using Webao;
using Webao.Attributes;
using Webao.Base;

namespace DynWebao
{
    public class WebaoDynBuilder
    {
        public static object Build(Type interfaceWebao, IRequest req)
        {
            var typeInformation = TypeInfoCache.Get(interfaceWebao);
            var baseUrl = typeInformation.GetClassAttributesByName("BaseUrl");
            var parametersByName = typeInformation.GetClassAttributesByName("AddParameter");

            if (baseUrl[0] is BaseUrlAttribute baseUrlAttribute) req.BaseUrl(baseUrlAttribute.host);

            foreach (var param in parametersByName)
                if (param is AddParameterAttribute parameterAttribute)
                    req.AddParameter(parameterAttribute.name, parameterAttribute.val);

            BuilderHelper builderHelper = new BuilderHelper();
            //TODO : CRIAR ModuleBuilder com do ASSEMBLY
            ModuleBuilder builder = builderHelper.getModuleBuilder(interfaceWebao.Name);

            //TODO : CRIAR TypeBuilder com especificação de classe
            TypeBuilder t = BuilderHelper.GetTypeBuilder(interfaceWebao,builder);

            Emitter emitter = new Emitter();
            var methods = interfaceWebao.GetMethods();
            foreach (var method in methods)
            {
                MethodInformation info = BuilderHelper.ProcessMethod(method);
                emitter.EmitMethod(t,info);
                 

            }

            return Activator.CreateInstance(interfaceWebao, req);
        }
    }
}