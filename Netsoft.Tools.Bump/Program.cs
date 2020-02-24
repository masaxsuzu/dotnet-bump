using CommandLine;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Netsoft.Tools.Bump
{
    public class Program
    {
        public static int Main(string[] args = null)
        {
            int exitcode = 0;
            var cmd = Parser.Default.ParseArguments<
                UpdateMajorVersionCommand,
                UpdateMinorVersionCommand,
                UpdatePatchVersionCommand>(args)
                .WithParsed<UpdateMajorVersionCommand>(upMajor => exitcode = Run(upMajor))
                .WithParsed<UpdateMinorVersionCommand>(upMinor => exitcode = Run(upMinor))
                .WithParsed<UpdatePatchVersionCommand>(upPatch => exitcode = Run(upPatch))
                .WithNotParsed(er => { /**/ });

            if (cmd.Tag != ParserResultType.Parsed)
            {
                return ExitWith($"Got argument error.", Console.Error);
            }

            return exitcode;
        }

        static int Run(ICommand command)
        {
            try
            {
                string newVersion = command.Update();
                Console.WriteLine(newVersion);
                return 0;
            }
            catch (ArgumentException ex)
            {
                return ExitWith(ex.Message, Console.Error);
            }
        }

        static int ExitWith(string error, TextWriter writer)
        {
            writer.WriteLine(error);
            return 1;
        }
    }
}
