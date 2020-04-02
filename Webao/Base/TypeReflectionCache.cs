using System;
using System.Collections.Generic;

namespace Webao.Base
{
    // Associates Types to their corresponding TypeInformation objects.
    // The latter contains reflection info (e.g., custom attributes info)
    // obtained only once.
    public class TypeInfoCache
    {
        private static readonly Dictionary<Type, TypeInformation> dict = new Dictionary<Type, TypeInformation>();

        public static TypeInformation Get(Type type)
        {
            // Check if the type 'type' was already processed.
            if (!dict.TryGetValue(type, out var typeInfo))
            {
                // If not yet consulted, create a TypeInformation object for
                // this type, and add the pair (type, typeInfo) to dictionary.
                // The TypeInformation contains reflection info (e.g., custom attributes info)
                // obtained only once.
                typeInfo = new TypeInformation(type);
                dict.Add(type, typeInfo);
            }
            else
            {
                typeInfo = dict[type];
            }

            // Return the TypeInformation (newly created only once or the existing one)
            // for the type 'type'.
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
                    //QUERO ADICIONAR Á LISTA DE ATRIUTOS ESTE ATRIBUTO
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

        public void SetAttributesList(string attributeName, List<Attribute> list)
        {
            if (classAttributes.ContainsKey(attributeName))
            {
                /*attributes[attributeName] = attributes[attributeName].Concat(list);*/
            }
        }

        /*public List<Attribute> GetAttributesByMethodName(string methodName)
        {
            return methodAttributes[methodName];
        }*/

        public List<Attribute> GetAttributesByMethodName(string methodName, string attributeName)
        {
            throw new NotImplementedException();
        }
    }
}