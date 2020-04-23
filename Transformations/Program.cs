using CommandLine;
using Serilog;
using System.IO;
using System.Linq;
using Transformations.Commands;
using Xbim.Common;
using Xbim.Ifc;
using Xbim.IO.Memory;

namespace Transformations
{
    class Program
    {
        public static void Main(string[] args)
        {

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    var config = new LoggerConfiguration()
                        .WriteTo.Console();
                    if (o.Verbose)
                    {
                        config = config.MinimumLevel.Verbose();
                    }
                    else
                    {
                        config = config.MinimumLevel.Warning();
                    }
                    Log.Logger = config.CreateLogger();
                    XbimLogging.LoggerFactory.AddSerilog();

                    var editor = new XbimEditorCredentials
                    {
                        ApplicationDevelopersName = o.OwningApplicationDeveloper,
                        ApplicationFullName = o.OwningApplication,
                        ApplicationIdentifier = o.OwningApplicationIdentifier,
                        ApplicationVersion = o.OwningApplicationVersion,
                        EditorsFamilyName = o.EditorSurname,
                        EditorsGivenName = o.EditorName,
                        EditorsOrganisationName = o.EditorOrganization
                    };

                    IfcStore.ModelProviderFactory.UseMemoryModelProvider();
                    using (var model = IfcStore.Open(o.Input, editor))
                    {
                        if (o.ResetOwner)
                            ResetOwner.Run(model, o);

                        if (o.ClearProperties)
                            ClearProperties.Run(model.Instances.First().Model as MemoryModel);

                        if (o.SetDSSCodes)
                            SetDSSCodes.Run(model);

                        if (o.AnonymizeElements)
                            ResetNames.Run(model);

                        if (o.ResetGuids)
                            ResetGuids.Run(model);

                        model.Header.FileName.Name = Path.GetFileName(o.Output);
                        model.SaveAs(o.Output);
                    }
                });
        }
    }
}
