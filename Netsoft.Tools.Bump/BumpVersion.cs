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
            int[] version = Split(current);
            version[_majorIndex]++;
            version[_minorIndex] = 0;
            version[_patchIndex] = 0;

            return string.Join('.', version);
        }

        public static string UpMinor(string current)
        {
            int[] version = Split(current);
            version[_minorIndex]++;
            version[_patchIndex] = 0;

            return string.Join('.', version);
        }

        public static string UpPatch(string current)
        {
            int[] version = Split(current);
            version[_patchIndex]++;
            return string.Join('.', version);
        }

        private static int[] Split(string current)
        {
            string[] version = current.Split(new char[] { '.' });
            if (version.Length <= _patchIndex)
            {
                throw new Exceptions.InvalidVersionSuppliedException("Version must be more than 3 digits.");
            }
            return version.Select(s =>
                {
                    if (!int.TryParse(s, out int n))
                    {
                        throw new Exceptions.InvalidVersionSuppliedException("Version must be more than 3 digits.");
                    }
                    return n;
                }).ToArray();
        }
    }
}
