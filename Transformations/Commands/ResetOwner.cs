using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xbim.Common;
using Xbim.Ifc4.Interfaces;

namespace Transformations.Commands
{
    internal static class ResetOwner
    {
        public static void Run(IModel model, Options options)
        {
            var i = model.Instances;
            using (var txn = model.BeginTransaction("Owner history set up"))
            {
                // enhance header
                // model.Header.FileDescription.Description.Clear(); // clear any MVD
                // model.Header.FileName.AuthorizationMailingAddress.Clear();
                // model.Header.FileName.AuthorizationMailingAddress.Add("info@xbim.net");
                // model.Header.FileName.AuthorizationName = "";
                // model.Header.FileName.AuthorName.Clear();
                // model.Header.FileName.AuthorName.Add("xBIM Team");
                // model.Header.FileName.Organization.Clear();
                // model.Header.FileName.Organization.Add("buildingSMART");
                model.Header.FileName.OriginatingSystem = "xBIM Toolkit";
                model.Header.FileName.PreprocessorVersion = typeof(IModel).Assembly.ImageRuntimeVersion;


                foreach (var history in i.OfType<IIfcOwnerHistory>())
                {
                    history.OwningApplication.ApplicationDeveloper.Addresses.Clear();
                    history.OwningApplication.ApplicationDeveloper.Description = options.OwningApplicationDeveloper;
                    history.OwningApplication.ApplicationDeveloper.Identification = options.OwningApplicationDeveloper;
                    history.OwningApplication.ApplicationDeveloper.Name = options.OwningApplicationDeveloper;
                    history.OwningApplication.ApplicationDeveloper.Roles.Clear();
                    history.OwningApplication.ApplicationFullName = options.OwningApplication;
                    history.OwningApplication.ApplicationIdentifier = options.OwningApplicationIdentifier;
                    history.OwningApplication.Version = options.OwningApplicationVersion;
                }

                txn.Commit();
            }
        }
    }
}
