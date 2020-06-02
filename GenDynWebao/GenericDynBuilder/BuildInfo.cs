using System;
using System.Collections.Generic;
using GenDynWebao.GenericDynBuilder;
using Webao;
using Webao.Attributes;

namespace GenericWebao.GenericDynBuilder
{
    public class BuildInfo<T>
    {
        private readonly string BaseUrl;
        private readonly List<MethodInformation> MethodInformations;
        private readonly List<AddParameterAttribute> ParameterAttributes;
        private MethodInformation CurrentMethod;


        public BuildInfo(string baseUrl)
        {
            BaseUrl = baseUrl;
            ParameterAttributes = new List<AddParameterAttribute>();
            MethodInformations = new List<MethodInformation>();
        }

        public BuildInfo<T> AddParameter(string tag, string value)
        {
            ParameterAttributes.Add(new AddParameterAttribute(tag, value));
            return this;
        }

        public BuildInfo<T> On(string methodName)
        {
            CurrentMethod = new MethodInformation {Name = methodName};
            return this;
        }

        public BuildInfo<T> GetFrom(string path)
        {
            CurrentMethod.Get = new GetAttribute(path);
            return this;
        }

        public BuildInfo<T> Mapping<TR>(Func<TR, object> func)
        {
            MethodInformation.Delegates.Add(func);
            CurrentMethod.Mapping = new MappingAttribute(typeof(TR));
            CurrentMethod = BuilderHelper.CompleteMethodInfo(typeof(T), CurrentMethod);
            MethodInformations.Add(CurrentMethod);
            return this;
        }

        public object Build(IRequest request)
        {
            request.BaseUrl(BaseUrl);
            foreach (var parameter in ParameterAttributes) request.AddParameter(parameter.name, parameter.val);
            return WebaoGenericBuilder.BuildTask(typeof(T), request, MethodInformations);
        }
    }
}