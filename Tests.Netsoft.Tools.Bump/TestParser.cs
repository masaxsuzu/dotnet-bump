using Netsoft.Versioning;

using System;
using Xunit;

namespace Tests.Netsoft.Versioning
{
    public class TestParser
    {
        [Theory]
        [InlineData("1.2", new int[] { 1, 2, 0 })]
        [InlineData("1.2.3", new int[] { 1, 2, 3 })]
        [InlineData("1.0.1", new int[] { 1, 0, 1 })]
        [InlineData("1.2.0", new int[] { 1, 2, 0 })]
        [InlineData("1.0.0", new int[] { 1, 0, 0 })]
        public void TestParse01(string s, int[] want)
        {
            var got = Parser.ParseVersion(s);

            Assert.Equal(
                new MyVersion(want[0], want[1])
                .WithPatchVersion(want[2]),
                got);
        }

        [Theory]
        [InlineData("1.2.3-y", new int[] { 1, 2, 3 }, "y")]
        [InlineData("1.0.1-z", new int[] { 1, 0, 1 }, "z")]
        [InlineData("1.2.0-alpha", new int[] { 1, 2, 0 }, "alpha")]
        [InlineData("1.0.0-beta", new int[] { 1, 0, 0 }, "beta")]
        public void TestParse02(string s, int[] want, string tag)
        {
            var got = Parser.ParseVersion(s);

            Assert.Equal(
                new MyVersion(want[0], want[1])
                .WithPatchVersion(want[2])
                .WithTagName(tag),
                got);
        }

        [Theory]
        [InlineData("1.2.3.4-y", new int[] { 1, 2, 3, 4 }, "y")]
        [InlineData("1.0.1.4-z", new int[] { 1, 0, 1, 4 }, "z")]
        [InlineData("1.2.0.4-alpha", new int[] { 1, 2, 0, 4 }, "alpha")]
        [InlineData("1.0.0.4-beta", new int[] { 1, 0, 0, 4 }, "beta")]
        public void TestParse03(string s, int[] want, string tag)
        {
            var got = Parser.ParseVersion(s);

            Assert.Equal(
                new MyVersion(want[0], want[1])
                .WithPatchVersion(want[2])
                .WithBuildVersion(want[3])
                .WithTagName(tag),
                got);
        }

        [Theory]
        [InlineData("0.2.3")]
        [InlineData("0.2")]
        [InlineData("1.2.")]
        [InlineData("1.2.x")]
        [InlineData("1.y.1")]
        public void TestParseError(string s)
        {
            Assert.Throws<ArgumentException>(() => _ = Parser.ParseVersion(s));
        }

        [Theory]
        [InlineData("1.2.3", 1, new string[] { "2", "3" })]
        [InlineData("0.2.3", 0, new string[] { "2", "3" })]
        public void TestParseNumber(string s, uint want, string[] next)
        {
            string[] s0 = s.Split('.');

            var (got, rest) = Parser.ParseNumber(s0);

            Assert.Equal(want, got);
            Assert.Equal(next, rest);
        }

        [Theory]
        [InlineData("-1", null, new string[] { "-1" })]
        [InlineData("s", null, new string[] { "s" })]
        [InlineData("", null, new string[] { "" })]
        public void TestParseNumberError(string s, uint? want, string[] next)
        {
            string[] s0 = s.Split('.');

            var (got, rest) = Parser.ParseNumber(s0);

            Assert.Equal(want, got);
            Assert.Equal(next, rest);
        }

        [Theory]
        [InlineData("1.2.3", 1, new string[] { "2", "3" })]
        public void TestParsePositiveNumber(string s, uint want, string[] next)
        {
            string[] s0 = s.Split('.');

            var (got, rest) = Parser.ParsePositiveNumber(s0);

            Assert.Equal(want, got);
            Assert.Equal(next, rest);
        }

        [Theory]
        [InlineData("0", null, new string[] { "0" })]
        [InlineData("-1", null, new string[] { "-1" })]
        [InlineData("s", null, new string[] { "s" })]
        [InlineData("", null, new string[] { "" })]
        public void TestParsePositiveNumberError(string s, uint? want, string[] next)
        {
            string[] s0 = s.Split('.');

            var (got, rest) = Parser.ParsePositiveNumber(s0);

            Assert.Equal(want, got);
            Assert.Equal(next, rest);
        }
    }
}
