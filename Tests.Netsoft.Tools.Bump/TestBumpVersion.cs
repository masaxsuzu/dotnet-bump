using Netsoft.Tools.Bump;
using System;
using Xunit;

namespace Tests.Netsoft.Tools.Bump
{
    public class TestBumpVersion
    {
        [Theory]
        [InlineData("1.2", "2.0")]
        [InlineData("1.2.3", "2.0")]
        [InlineData("1.2.3.4", "2.0")]
        [InlineData("1.2.3-x", "2.0-x")]
        [InlineData("1.2.3.4-x", "2.0-x")]

        public void TestUpMajor(string from, string want)
        {
            string got = BumpVersion.UpMajor(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1.2", "1.3")]
        [InlineData("1.2.3", "1.3")]
        [InlineData("1.2.3.4", "1.3")]
        [InlineData("1.2.3-x", "1.3-x")]
        [InlineData("1.2.3.4-x", "1.3-x")]

        public void TestUpMinor(string from, string want)
        {
            string got = BumpVersion.UpMinor(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1.2", "1.2.1")]
        [InlineData("1.2.3", "1.2.4")]
        [InlineData("1.2.3.4", "1.2.4")]
        [InlineData("1.2.3-x", "1.2.4-x")]
        [InlineData("1.2.3.4-x", "1.2.4-x")]

        public void TestUpPatch(string from, string want)
        {
            string got = BumpVersion.UpPatch(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1.2", "1.2.0.1")]
        [InlineData("1.2.3", "1.2.3.1")]
        [InlineData("1.2.3.4", "1.2.3.5")]
        [InlineData("1.2.3-x", "1.2.3.1-x")]
        [InlineData("1.2.3.4-x", "1.2.3.5-x")]

        public void TestUpBuild(string from, string want)
        {
            string got = BumpVersion.UpBuild(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1.2", "1.2")]
        [InlineData("1.2.0", "1.2")]
        [InlineData("1.2.0.1", "1.2.0.1")]
        [InlineData("1.2.3", "1.2.3")]
        [InlineData("1.2.3.4", "1.2.3.4")]
        [InlineData("1.2.3-x", "1.2.3-x")]
        [InlineData("1.2.3.4-x", "1.2.3.4-x")]

        public void TestFormat(string from, string want)
        {
            string got = BumpVersion.Format(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1.x")]
        [InlineData("y")]
        public void TestErrorIfInvalidVersionSupplied(string from)
        {
            _ = Assert.Throws<ArgumentException>(() => _ = BumpVersion.UpMajor(from));
            _ = Assert.Throws<ArgumentException>(() => _ = BumpVersion.UpMinor(from));
            _ = Assert.Throws<ArgumentException>(() => _ = BumpVersion.UpPatch(from));
            _ = Assert.Throws<ArgumentException>(() => _ = BumpVersion.UpBuild(from));
            _ = Assert.Throws<ArgumentException>(() => _ = BumpVersion.Format(from));
        }

    }
}
