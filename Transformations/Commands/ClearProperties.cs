using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xbim.Common;
using Xbim.Common.Model;
using Xbim.Ifc4.Interfaces;

namespace Transformations.Commands
{
    internal static class ClearProperties
    {
        public static void Run(StepModel model)
        {
            var toDelete = Array.Empty<IPersistEntity>()
                .Concat(model.Instances.OfType<IIfcRelDefinesByProperties>())
                .Concat(model.Instances.OfType<IIfcPropertySetDefinition>())
                .Concat(model.Instances.OfType<IIfcPropertyAbstraction>())
                .ToArray();

            model.Delete(toDelete, true);
        }
    }
}
