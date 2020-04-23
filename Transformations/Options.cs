using CommandLine;

namespace Transformations
{
    public class Options
    {
        [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.", Default = false)]
        public bool Verbose { get; set; }

        [Option('i', "input", Required = true, HelpText = "Input IFC file")]
        public string Input { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output IFC file")]
        public string Output { get; set; }

        [Option('r', "reset-owning-application", Required = false, HelpText = "Resets application in owner history", Default = true)]
        public bool ResetOwner { get; set; }

        [Option("owning-application-developper", Required = false, HelpText = "Resets application in owner history", Default = "xBIM Team")]
        public string OwningApplicationDeveloper { get; set; }

        [Option("owning-application", Required = false, HelpText = "Resets application in owner history", Default = "xBIM Toolkit")]
        public string OwningApplication { get; set; }

        [Option("owning-application-id", Required = false, HelpText = "Resets application in owner history", Default = "XBIM")]
        public string OwningApplicationIdentifier { get; set; }

        [Option("owning-application-version", Required = false, HelpText = "Resets application in owner history", Default = "XBIM")]
        public string OwningApplicationVersion { get; set; }

        [Option('d', "dss-codes", Required = false, HelpText = "Sets DSS codes as properties based on IFC type", Default = true)]
        public bool SetDSSCodes { get; set; }

        [Option('e', "editor-name", Required = false, HelpText = "Name of the editor of the file", Default = "Editor")]
        public string EditorName { get; set; }

        [Option("editor-surname", Required = false, HelpText = "Name of the editor of the file", Default = "")]
        public string EditorSurname { get; set; }

        [Option("editor-organization", Required = false, HelpText = "Name of the editor of the file", Default = "Česká agentura pro standardizaci")]
        public string EditorOrganization { get; set; }

        [Option('a', "anonymize-elements", Required = false, HelpText = "Set random names to all elements", Default = true)]
        public bool AnonymizeElements { get; set; }

        [Option('c', "clear-all-properties", Required = false, HelpText = "Deletes all properties and property sets before other processing", Default = true)]
        public bool ClearProperties { get; set; }

        [Option('g', "reset-guids", Required = false, HelpText = "Creates new GUID for all IfcRoot entities", Default = true)]
        public bool ResetGuids { get; set; }
    }
}
