using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Netsoft.Tools.Bump
{
    public static class BumpVersion
    {
        const int _majorIndex = 0;
        const int _minorIndex = 1;
        const int _patchIndex = 2;

        public static string UpMajor(string current)
        {
            var version = TryFormat(current);
            return new Version(
                version.Major + 1,
                0,
                0,
                Math.Max(0, version.Revision)).ToString();
        }

        public static string UpMinor(string current)
        {
            var version = TryFormat(current);
            return new Version(
                version.Major,
                version.Minor + 1,
                0,
                Math.Max(0, version.Revision)).ToString();
        }

        public static string UpPatch(string current)
        {
            var version = TryFormat(current);
            return new Version(
                version.Major,
                version.Minor,
                Math.Max(0, version.Build) + 1,
                Math.Max(0, version.Revision)).ToString();
        }

        private static Version TryFormat(string current)
        {
            try
            {
                return new Version(current);
            }
            catch(System.ArgumentException)
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
