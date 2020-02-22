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

        [InlineData(1, "major", "1.2.x")]
        [InlineData(1, "major")]
        [InlineData(1, "1.2")]
        public void TestMain(int want, params string[] args)
        {
            int got = Program.Main(args);

            Assert.Equal(want, got);
        }
    }
}
