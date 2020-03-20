using CommandLine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Netsoft.Tools.Bump
{
    interface ICommand
    {
        string Update();
    }

    [Verb("major", HelpText = "Update major version")]
    class UpdateMajorVersionCommand : ICommand
    {
        [Value(0, MetaName = "version", Required = true, HelpText = "(e.g. 1.2, 1.2.3, 1.2.3.4)")]

        public string Version { get; set; }

        public string Update()
        {
            return BumpVersion.UpMajor(Version);
        }
    }

    [Verb("minor", HelpText = "Update minor version")]
    class UpdateMinorVersionCommand : ICommand
    {
        [Value(0, MetaName = "version", Required = true, HelpText = "(e.g. 1.2, 1.2.3, 1.2.3.4)")]
        public string Version { get; set; }

        public string Update()
        {
            return BumpVersion.UpMinor(Version);
        }
    }

    [Verb("patch", HelpText = "Update patch version")]
    class UpdatePatchVersionCommand : ICommand
    {
        [Value(0, MetaName = "version", Required = true, HelpText = "(e.g. 1.2, 1.2.3, 1.2.3.4)")]
        public string Version { get; set; }
        public string Update()
        {
            return BumpVersion.UpPatch(Version);
        }
    }

    [Verb("build", HelpText = "Update build version")]
    class UpdateBuildVersionCommand : ICommand
    {
        [Value(0, MetaName = "version", Required = true, HelpText = "(e.g. 1.2, 1.2.3, 1.2.3.4)")]
        public string Version { get; set; }
        public string Update()
        {
            return BumpVersion.UpBuild(Version);
        }
    }
    
    [Verb("format", HelpText = "Format version")]
    class FormatVersionCommand : ICommand
    {
        [Value(0, MetaName = "version", Required = true, HelpText = "(e.g. 1.2, 1.2.3, 1.2.3.4)")]
        public string Version { get; set; }
        public string Update()
        {
            return BumpVersion.Format(Version);
        }
    }
}
