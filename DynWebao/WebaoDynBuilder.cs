using System;
using Webao;
using Webao.Attributes;
using Webao.Base;

namespace DynWebao
{
    public class WebaoDynBuilder
    {
        private static BuilderHelper builderHelper = new BuilderHelper();
        private static Emitter emitter = new Emitter();
        
        public static object Build(Type interfaceWebao, IRequest req)
        {
            var typeInformation = TypeInfoCache.Get(interfaceWebao);
            var baseUrl = typeInformation.GetClassAttributesByName("BaseUrl");
            var parametersByName = typeInformation.GetClassAttributesByName("AddParameter");

            if (baseUrl[0] is BaseUrlAttribute baseUrlAttribute) req.BaseUrl(baseUrlAttribute.host);

            foreach (var param in parametersByName)
                if (param is AddParameterAttribute parameterAttribute)
                    req.AddParameter(parameterAttribute.name, parameterAttribute.val);

            builderHelper.SetModuleBuilder(interfaceWebao.Name.Remove(0,1));
            builderHelper.SetTypeBuilder(interfaceWebao); 
            emitter.EmitConstructor(builderHelper.TypeBuilder,builderHelper.GetBaseCtor());
            
            var methods = interfaceWebao.GetMethods();
            
            foreach (var method in methods)
            {
                var info = builderHelper.ProcessMethod(method);
                emitter.EmitMethod(builderHelper.TypeBuilder, info);
            }

            var type = builderHelper.TypeBuilder.CreateType();
            builderHelper.Save();
            return Activator.CreateInstance(type, req);
        }
        
    }
}