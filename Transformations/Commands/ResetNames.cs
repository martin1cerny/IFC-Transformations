using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xbim.Common;
using Xbim.Ifc4.Interfaces;

namespace Transformations.Commands
{
    internal static class ResetNames
    {
        public static void Run(IModel model)
        {
            using (var txn = model.BeginTransaction("Reset all element names"))
            {
                var toProcess = Array.Empty<IIfcRoot>()
                    .Concat(model.Instances.OfType<IIfcElement>())
                    .Concat(model.Instances.OfType<IIfcElementType>());

                // reset names
                foreach (var item in toProcess)
                    item.Name = GetName(item.ExpressType.Type);

                // clear object type if it is not user defined type
                foreach (var element in model.Instances.OfType<IIfcElement>())
                {
                    if (!IsUserDefinedType(element))
                        element.ObjectType = "";
                }

                // clear object type if it is not user defined type
                foreach (var elementType in model.Instances.OfType<IIfcElementType>())
                {
                    if (!IsUserDefinedType(elementType))
                        elementType.ElementType = "";
                }

                txn.Commit();
            }
        }

        private static Dictionary<Type, string> NamesMap { get; } = new Dictionary<Type, string>();
        private static Dictionary<Type, int> Counters { get; } = new Dictionary<Type, int>();

        private static bool IsUserDefinedType(IPersistEntity entity)
        {
            var property = entity.ExpressType.Properties
                .FirstOrDefault(p => string.Equals(p.Value.Name, "PredefinedType", StringComparison.OrdinalIgnoreCase));
            if (property.Value == null)
                return false;
            var pInfo = property.Value.PropertyInfo;
            var value = pInfo.GetValue(entity);
            if (value == null)
                return false;

            var pType = pInfo.PropertyType;
            if (pType.IsGenericType && pType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                var inner = Nullable.GetUnderlyingType(pInfo.PropertyType);
                var hasValInfo = pInfo.PropertyType.GetProperty(nameof(Nullable<int>.HasValue));
                if ((bool)hasValInfo.GetValue(value) == false)
                    return false;
                
                var valInfo = pInfo.PropertyType.GetProperty(nameof(Nullable<int>.Value));
                value = valInfo.GetValue(value);
                pType = pType.GetGenericArguments()[0];
            }

            return string.Equals(value.ToString(), "USERDEFINED", StringComparison.OrdinalIgnoreCase);
        }

        private static string GetName(Type type)
        {
            if (!NamesMap.TryGetValue(type, out string name))
            {
                name = type.Name;
                
                // strip out 'IFC'
                if (name.StartsWith("ifc", StringComparison.OrdinalIgnoreCase))
                    name = name.Substring(3);

                // split camel case
                name = Regex.Replace(name, "([A-Z])", " $1", RegexOptions.Compiled).Trim();

                NamesMap.Add(type, name);
            }

            if (!Counters.TryGetValue(type, out int count))
            { 
                count = 0;
                Counters.Add(type, count);
            }

            count++;
            Counters[type] = count;

            return $"{name} {count:D5}";
        }
    }
}
