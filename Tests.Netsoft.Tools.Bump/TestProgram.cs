using Netsoft.Tools.Bump;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.Netsoft.Tools.Bump
{
    public class TestProgram
    {
        [Theory]
        [InlineData(0, "major", "1.2.3")]
        [InlineData(0, "minor", "1.2.3")]
        [InlineData(0, "patch", "1.2.3")]
        [InlineData(0, "major", "1.2")]
        [InlineData(0, "format", "1.2")]

        public void TestMain(int want, params string[] args)
        {
            int got = Program.Main(args);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData(1, "major", "1.2.x")]
        [InlineData(1, "major")]
        [InlineData(1, "1.2")]
        [InlineData(1, "format")]
        [InlineData(2, "version")]
        [InlineData(2, "--version")]
        [InlineData(2, "help")]
        [InlineData(2, "--help")]
        public void TestMainError(int want, params string[] args)
        {
            int got = Program.Main(args);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("2.0\r\n", "major", "1.2.3")]
        [InlineData("1.3\r\n", "minor", "1.2.3")]
        [InlineData("1.2.4\r\n", "patch", "1.2.3")]
        [InlineData("1.2.3\r\n", "format", "1.2.3.0")]
        public void TestConsoleOut(string want, params string[] args)
        {
            using (var mem = new System.IO.MemoryStream())
            {
                using (var stdout = new System.IO.StreamWriter(mem))
                {
                    Console.SetOut(stdout);

                    _ = Program.Main(args);
                }
                string got = Encoding.UTF8.GetString(mem.ToArray());
                Assert.Equal(want, got);
            }
        }

        [Theory]
        [InlineData("Version string was less than 2 digits.\r\n", "major", "1")]
        [InlineData("Input string was not in a correct format.\r\n", "major", "1.2.x")]
        [InlineData("Got argument error.\r\n", "major")]
        [InlineData("", "help")]
        [InlineData("", "version")]
        public void TestConsoleError(string want, params string[] args)
        {
            using (var mem = new System.IO.MemoryStream())
            {
                using (var stderr = new System.IO.StreamWriter(mem))
                {
                    Console.SetError(stderr);

                    _ = Program.Main(args);
                }
                string got = Encoding.UTF8.GetString(mem.ToArray());
                Assert.Equal(want, got);
            }
        }
    }
}