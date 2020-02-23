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
            return TryFormat(current)
                .Into()
                .UpMajorVersion()
                .ToString();
        }

        public static string UpMinor(string current)
        {
            return TryFormat(current)
                .Into()
                .UpMinorVersion()
                .ToString();
        }

        public static string UpPatch(string current)
        {
            return TryFormat(current)
                .Into()
                .UpPatchVersion()
                .ToString();
        }

        private static MyVersion Into(this Version version)
        {
            return new Versioning.MyVersion(version.Major, version.Minor)
                .WithPatchVersion(version.Build)
                .WithBuildVersion(version.Revision);
        }

        private static Version TryFormat(string current)
        {
            try
            {
                return new Version(current);
            }
            catch (System.ArgumentException)
            {
                throw new Exceptions.InvalidVersionSuppliedException("Version string was less than 2 digits.");
            }
            catch (System.FormatException ex)
            {
                throw new Exceptions.InvalidVersionSuppliedException(ex.Message);
            }
        }
    }
}
