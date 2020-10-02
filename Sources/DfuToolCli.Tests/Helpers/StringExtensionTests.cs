using DfuToolCli.Helpers;
using NUnit.Framework;
using System;

namespace DfuToolCli.Tests.Helpers {
    [TestFixture]
    public class StringExtensionTests {
        [TestCase("1", 0, 0xffff, ExpectedResult = 1)]
        [TestCase("-1", -1, 10, ExpectedResult = -1)]
        [TestCase("0x1234", 0, 0xffff, ExpectedResult = 0x1234)]
        [TestCase("100500", 0, 1000000, ExpectedResult = 100500)]
        [TestCase("2147483647", int.MinValue, int.MaxValue, ExpectedResult = 2147483647)]
        [TestCase("-2147483648", int.MinValue, int.MaxValue, ExpectedResult = -2147483648)]
        public int CanParseInt32(string input, int min, int max) => input.ToInt32(min, max);

        [TestCase("-11", 0, 0xffff)]
        [TestCase("-10", -1, 10)]
        [TestCase("2", 5, 10)]
        [TestCase("0xx1234", 0, 0xffff)]
        [TestCase("100500h", 0, 1000000)]
        [TestCase("x100500", 0, 1000000)]
        [TestCase("2147483648", int.MinValue, int.MaxValue)]
        [TestCase("-2147483649", int.MinValue, int.MaxValue)]
        public void CannotParseInt32(string input, int min, int max) {
            Assert.Throws<ArgumentException>(() => input.ToInt32(min, max));
        }

        [TestCase("1", 0u, 0xffffu, ExpectedResult = 1u)]
        [TestCase("0x1234", 0u, 0xffffu, ExpectedResult = 0x1234u)]
        [TestCase("100500", 0u, 1000000u, ExpectedResult = 100500u)]
        [TestCase("2147483647", uint.MinValue, uint.MaxValue, ExpectedResult = 2147483647u)]
        [TestCase("2147483648", uint.MinValue, uint.MaxValue, ExpectedResult = 2147483648u)]
        [TestCase("4000000000", uint.MinValue, uint.MaxValue, ExpectedResult = 4000000000u)]
        public uint CanParseUInt32(string input, uint min, uint max) => input.ToUInt32(min, max);

        [TestCase("-11", 0u, 0xffffu)]
        [TestCase("2", 5u, 10u)]
        [TestCase("0xx1234", 0u, 0xffffu)]
        [TestCase("100500h", 0u, 1000000u)]
        [TestCase("x100500", 0u, 1000000u)]
        public void CannotParseUInt32(string input, uint min, uint max) {
            Assert.Throws<ArgumentException>(() => input.ToUInt32(min, max));
        }
    }
}
