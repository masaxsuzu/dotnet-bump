using System;
using System.IO;
using System.Threading.Tasks;

namespace Netsoft.Tools.Bump
{
    public class Program
    {
        public static int Main(string[] args = null)
        {
            if (args.Length < 2)
            {
                return ExitWith($"Arguments must be 2, but {args.Length} supplied.", Console.Error);
            }

            string command = args[0];
            string currentVersion = args[1];

            try
            {
                string newVersion = command switch
                {
                    "major" => BumpVersion.UpMajor(currentVersion),
                    "minor" => BumpVersion.UpMinor(currentVersion),
                    "patch" => BumpVersion.UpPatch(currentVersion),

                    _ => currentVersion,
                };

                Console.WriteLine(newVersion);
            }
            catch (Exceptions.InvalidVersionSuppliedException ex)
            {
                return ExitWith(ex.Message, Console.Error);
            }

            return 0;
        }

        static int ExitWith(string error, TextWriter writer)
        {
            writer.WriteLine(error);
            return 1;
        }
    }
}
