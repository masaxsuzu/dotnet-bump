using Netsoft.Tools.Bump;
using Netsoft.Tools.Bump.Exceptions;
using System;
using Xunit;

namespace Tests.Netsoft.Tools.Bump
{
    public class TestBumpVersion
    {
        [Theory]
        [InlineData("1.2.3","2.0.0")]
        public void TestUpMajor(string from, string want)
        {
            string got = BumpVersion.UpMajor(from);

            Assert.Equal(want,got);
        }

        [Theory]
        [InlineData("1.2.3", "1.3.0")]
        public void TestUpMinor(string from, string want)
        {
            string got = BumpVersion.UpMinor(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1.2.3", "1.2.4")]
        public void TestUpPatch(string from, string want)
        {
            string got = BumpVersion.UpPatch(from);

            Assert.Equal(want, got);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("1.0")]
        [InlineData("x")]
        public void TestErrorIfInvalidVersionSupplied(string from)
        {
            _ = Assert.Throws<InvalidVersionSuppliedException>(() => _ = BumpVersion.UpMajor(from));
        }

    }
}
