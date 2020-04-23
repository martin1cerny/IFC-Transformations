using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xbim.Common;
using Xbim.Ifc4.Interfaces;
using Xbim.Ifc4.MeasureResource;

namespace Transformations.Commands
{
    internal static class SetDSSCodes
    {
        public static void Run(IModel model)
        {
            using (var txn = model.BeginTransaction("DSS Codes"))
            {
                SetCodes<IIfcColumn>(model, "52458", "Sloup");
                SetCodes<IIfcDoor>(model, "52459", "Dveře");
                SetCodes<IIfcWall>(model, "52460", "Stěna");
                SetCodes<IIfcSlab>(model, "52462", "Deska");
                SetCodes<IIfcBeam>(model, "62891", "Nosník");
                SetCodes<IIfcBuildingElementProxy>(model, "62893", "Obecný stavební prvek");
                SetCodes<IIfcChimney>(model, "62894", "Komín");
                SetCodes<IIfcCurtainWall>(model, "62896", "Lehký obvodový plášť");
                SetCodes<IIfcFooting>(model, "62897", "Základová konstrukce");
                SetCodes<IIfcMember>(model, "62898", "Liniový prvek");
                SetCodes<IIfcPile>(model, "62899", "Pilota");
                SetCodes<IIfcPlate>(model, "62900", "Plošný element");
                SetCodes<IIfcRailing>(model, "62901", "Zábradlí");
                SetCodes<IIfcRamp>(model, "62902", "Rampa");
                SetCodes<IIfcRampFlight>(model, "62903", "Šikmé rameno rampy");
                SetCodes<IIfcRoof>(model, "62904", "Střecha / střešní plášť");
                SetCodes<IIfcShadingDevice>(model, "62905", "Stínící element");
                SetCodes<IIfcStair>(model, "62906", "Schodiště");
                SetCodes<IIfcStairFlight>(model, "62907", "Schodišťové rameno");
                SetCodes<IIfcWindow>(model, "62908", "Okno");
                
                txn.Commit();
            }
        }

        private static void SetCodes<T>(IModel model, string code, string description) where T : IIfcObject
        {
            var objects = model.Instances.OfType<T>();
            if (!objects.Any())
                return;

            var c = new Create(model);
            c.RelDefinesByProperties(r =>
            {
                r.RelatingPropertyDefinition = c.PropertySet(ps =>
                {
                    ps.Name = "CZ_DataTemplateDesignation";
                    ps.HasProperties.AddRange(new[] {
                        c.PropertySingleValue(p =>
                        {
                            p.Name = "DataTemplate ID";
                            p.NominalValue = new IfcLabel(code);
                        }),
                        c.PropertySingleValue(p =>
                        {
                            p.Name = "DataTemplateDescription";
                            p.NominalValue = new IfcText(description);
                        })
                    });
                });
                r.RelatedObjects.AddRange(objects.Cast<IIfcObject>());
            });
        }
    }
}
