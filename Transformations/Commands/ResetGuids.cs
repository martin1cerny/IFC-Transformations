using System;
using System.Collections.Generic;
using System.Text;
using Xbim.Common;
using Xbim.Ifc4.Interfaces;

namespace Transformations.Commands
{
    internal static class ResetGuids
    {
        public static void Run(IModel model)
        {
            using (var txn = model.BeginTransaction("Reset GUIDs"))
            {
                foreach (var root in model.Instances.OfType<IIfcRoot>())
                    root.GlobalId = Guid.NewGuid();
                txn.Commit();
            }
        }
    }
}
