using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Netsoft.Versioning;

namespace Netsoft.Tools.Bump
{
    public static class BumpVersion
    {
        public static string UpMajor(string current)
        {
            return Parser.ParseVersion(current)
                .UpMajorVersion()
                .Format()
                .ToString();
        }

        public static string UpMinor(string current)
        {
            return Parser.ParseVersion(current)
                .UpMinorVersion()
                .Format()
                .ToString();
        }

        public static string UpPatch(string current)
        {
            return Parser.ParseVersion(current)
                .UpPatchVersion()
                .Format()
                .ToString();
        }

        public static string UpBuild(string current)
        {
            return Parser.ParseVersion(current)
                .UpBuildVersion()
                .Format()
                .ToString();
        }

        public static string Format(string current)
        {
            return Versioning.Parser.ParseVersion(current)
                .Format();
        }
    }
}
