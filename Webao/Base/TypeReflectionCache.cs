using System;
using System.Collections.Generic;

namespace Webao.Base
{
    public class TypeInfoCache
    {
        private static readonly Dictionary<Type, TypeInformation> dict = new Dictionary<Type, TypeInformation>();

        public static TypeInformation Get(Type type)
        {
            if (!dict.TryGetValue(type, out var typeInfo))
            {
                typeInfo = new TypeInformation(type);
                dict.Add(type, typeInfo);
            }
            else
            {
                typeInfo = dict[type];
            }

            return typeInfo;
        }
    }

    public class TypeInformation
    {
        private static Dictionary<string, List<Attribute>> classAttributes;
        private readonly Type targetType;

        public TypeInformation(Type targetType)
        {
            this.targetType = targetType;
            classAttributes = new Dictionary<string, List<Attribute>>();
            ProcessClassAttributes();
        }

        private void ProcessClassAttributes()
        {
            var attributesList = targetType.GetCustomAttributes(false);
            foreach (var att in attributesList)
            {
                var attributeName = att.GetType().Name.Replace("Attribute", "");
                if (classAttributes.ContainsKey(attributeName))
                {
                    classAttributes[attributeName].Add(att as Attribute);
                }
                else
                {
                    var newList = new List<Attribute>();
                    newList.Add(att as Attribute);
                    classAttributes[attributeName] = newList;
                }
            }
        }

        public List<Attribute> GetClassAttributesByName(string attributeName)
        {
            return classAttributes[attributeName];
        }
    }
}