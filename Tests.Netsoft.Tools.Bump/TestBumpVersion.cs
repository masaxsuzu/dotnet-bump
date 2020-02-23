using Netsoft.Tools.Bump;
using Netsoft.Tools.Bump.Exceptions;
using System;
using Xunit;

namespace Tests.Netsoft.Tools.Bump
{
    public class TestBumpVersion
    {
        [Theory]
        [InlineData("1.2", "2.0.0.0")]
        [InlineData("1.2.3", "2.0.0.0")]
        [InlineData("1.2.3.4", "2.0.0.4")]
        public void TestUpMajor(string from, string want)
        {
            string got = BumpVersion.UpMajor(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1.2", "1.3.0.0")]
        [InlineData("1.2.3", "1.3.0.0")]
        [InlineData("1.2.3.4", "1.3.0.4")]
        public void TestUpMinor(string from, string want)
        {
            string got = BumpVersion.UpMinor(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1.2", "1.2.1.0")]
        [InlineData("1.2.3", "1.2.4.0")]
        [InlineData("1.2.3.4", "1.2.4.4")]
        public void TestUpPatch(string from, string want)
        {
            string got = BumpVersion.UpPatch(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1.x")]
        [InlineData("y")]
        public void TestErrorIfInvalidVersionSupplied(string from)
        {
            _ = Assert.Throws<InvalidVersionSuppliedException>(() => _ = BumpVersion.UpMajor(from));
            _ = Assert.Throws<InvalidVersionSuppliedException>(() => _ = BumpVersion.UpMinor(from));
            _ = Assert.Throws<InvalidVersionSuppliedException>(() => _ = BumpVersion.UpPatch(from));
        }

    }
}
