using System;
using Webao.Attributes;
using Webao.Base;

namespace Webao
{
    public class WebaoBuilder
    {
        public static AbstractAccessObject Build(Type webao, IRequest req)
        {
            /*
             * !!!! TODO: Read and process annotations !!!!
             */
            var typeInformation = TypeInfoCache.Get(webao);
            var baseUrl = typeInformation.GetClassAttributesByName("BaseUrl");
            var parametersByName = typeInformation.GetClassAttributesByName("AddParameter");

            if (baseUrl[0] is BaseUrlAttribute baseUrlAttribute) req.BaseUrl(baseUrlAttribute.host);

            foreach (var param in parametersByName)
                if (param is AddParameterAttribute parameterAttribute)
                    req.AddParameter(parameterAttribute.name, parameterAttribute.val);
            return (AbstractAccessObject) Activator.CreateInstance(webao, req);
        }
    }
}