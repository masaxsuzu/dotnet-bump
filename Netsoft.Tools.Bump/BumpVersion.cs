using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Netsoft.Versioning;

namespace Netsoft.Tools.Bump
{
    public static class BumpVersion
    {
        const int _majorIndex = 0;
        const int _minorIndex = 1;
        const int _patchIndex = 2;

        public static string UpMajor(string current)
        {
            return TryFormat(current)
                .Into()
                .UpMajor()
                .ToString();
        }

        public static string UpMinor(string current)
        {
            return TryFormat(current)
                .Into()
                .UpMinor()
                .ToString();
        }

        public static string UpPatch(string current)
        {
            return TryFormat(current)
                .Into()
                .UpPatch()
                .ToString();
        }

        private static MyVersion Into(this Version version)
        {
            return new Versioning.MyVersion(version.Major, version.Minor)
                .WithPatch(version.Build)
                .WithBuild(version.Revision);
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
