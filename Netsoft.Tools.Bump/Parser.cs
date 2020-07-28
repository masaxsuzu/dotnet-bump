using System;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Tests.Netsoft.Versioning")]

namespace Netsoft.Versioning
{
    public static class Parser
    {
        public static MyVersion ParseVersion(string source)
        {
            string[] s = source.Split(".");

            var (v, rest) = ParseVersion(s);

            return v;
        }

        public static (MyVersion, string[]) ParseVersion(string[] source)
        {
            var (major, rest0) = ParsePositiveNumber(source);

            if (major == null)
            {
                throw new ArgumentException("Version string was less than 2 digits.");
            }

            var (minor, rest1) = ParseNumber(rest0);

            if (minor == null)
            {
                throw new ArgumentException("Version string was less than 2 digits.");
            }

            if (rest1.Length == 0)
            {
                return (new MyVersion((int)major, (int)minor)
                .WithPatchVersion(0),
                rest1);
            }

            var (patch, rest2) = ParseNumberWithTag(rest1);

            if (!patch.HasValue)
            {
                throw new ArgumentException("Input string was not in a correct format.");
            }

            if (rest2.Length == 0)
            {
                return (new MyVersion((int)major, (int)minor)
                .WithPatchVersion((int)patch.Value.Item1)
                .WithTagName(patch.Value.Item2),
                rest2);
            }

            var (build, rest3) = ParseNumberWithTag(rest2);

            if (!build.HasValue)
            {
                throw new ArgumentException("Input string was not in a correct format.");
            }

            return (
                new MyVersion((int)major, (int)minor)
                .WithPatchVersion((int)patch.Value.Item1)
                .WithBuildVersion((int)build.Value.Item1)
                .WithTagName(build.Value.Item2),
                rest3);
        }

        public static (uint?, string[]) ParseNumber(string[] source)
        {
            if (source == null || source.Length == 0)
            {
                return (null, source);
            }
            if (!uint.TryParse(source[0], out uint n))
            {
                return (null, source);
            }

            return (n, source[1..^0]);
        }

        public static ((uint, string)?, string[]) ParseNumberWithTag(string[] source) 
        {
            if (source == null || source.Length == 0)
            {
                return (null, source);
            }

            string[] x = source[0].Split("-");
            string tag = x.Length > 1 ? x[1] : "";
            if (!uint.TryParse(x[0], out uint n))
            {
                return (null, source);
            }

            return ((n, tag), source[1..^0]);
        }

        public static (uint?, string[]) ParsePositiveNumber(string[] source)
        {
            var (n, rest) = ParseNumber(source);

            if (n == 0)
            {
                return (null, source);
            }

            return (n, rest);
        }
    }
}