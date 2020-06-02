using System;
using System.Collections.Generic;
using System.Linq;
using GenDynWebao.GenericDynBuilder;
using Webao;
using Webao.Attributes;
using Webao.Base;

namespace GenericWebao.GenericDynBuilder
{
    public class WebaoGenericBuilder
    {
        private static readonly BuilderHelper builderHelper = new BuilderHelper();
        private static readonly Emitter emitter = new Emitter();

        public static object Build(Type interfaceWebao, IRequest req)
        {
            var typeInformation = TypeInfoCache.Get(interfaceWebao);
            var baseUrl = typeInformation.GetClassAttributesByName("BaseUrl");
            var parametersByName = typeInformation.GetClassAttributesByName("AddParameter");
            if (baseUrl[0] is BaseUrlAttribute baseUrlAttribute) req.BaseUrl(baseUrlAttribute.host);

            foreach (var param in parametersByName)
                if (param is AddParameterAttribute parameterAttribute)
                    req.AddParameter(parameterAttribute.name, parameterAttribute.val);


            var typeMethods = interfaceWebao.GetMethods();
            var methodInformation = typeMethods.Select(BuilderHelper.ProcessMethod).ToList();

            return BuildTask(interfaceWebao, req, methodInformation);
        }

        public static object BuildTask(Type inputType, IRequest req, IEnumerable<MethodInformation> methods)
        {
            builderHelper.SetModuleBuilder(inputType.Name.Remove(0, 1));
            builderHelper.SetTypeBuilder(inputType);
            emitter.EmitConstructor(builderHelper.TypeBuilder, BuilderHelper.GetBaseCtor());
            foreach (var method in methods)
            {
                try
                {
                    var del = MethodInformation.Delegates[MethodInformation.Index];
                }
                catch (ArgumentOutOfRangeException)
                {
                    emitter.EmitMethod(builderHelper.TypeBuilder, method, -1);
                    MethodInformation.Index++;
                }

                emitter.EmitMethod(builderHelper.TypeBuilder, method, MethodInformation.Index++);
            }

            var type = builderHelper.TypeBuilder.CreateType();
            builderHelper.Save();
            return Activator.CreateInstance(type, req);
        }

        public static BuildInfo<T> For<T>(string baseUrl)
        {
            return new BuildInfo<T>(baseUrl);
        }
    }
}