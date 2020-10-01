using DfuConvLib.Helpers;
using DfuToolCli.Tools.Dfus.Create;
using NUnit.Framework;
using System;
using System.IO;

namespace DfuToolCli.Tests.Tools.Create {
    [TestFixture]
    public class ProcessorTests : BaseTestFixture {
        [Test]
        public void CanProcessInternal() {
            var expected = new byte[] {
                0x44, 0x66, 0x75, 0x53, 0x65,
                0x01,
                0x00, 0x00, 0x00, 0x00,
                0x00,

                0x78, 0x56,
                0x34, 0x12,
                0x83, 0x04,
                0x1A, 0x01,
                0x55, 0x46, 0x44,
                0x10,
                0x00, 0x00, 0x00, 0x00
            };

            var length = expected.Length - 16;
            Array.Copy(
                BitConverter.GetBytes(length),
                0,
                expected,
                6,
                4);

            var crc32 = Crc32.ComputeAll(expected, 0, Convert.ToUInt32(expected.Length - 4));
            Array.Copy(
                BitConverter.GetBytes(crc32 ^ 0xffffffffu),
                0,
                expected,
                expected.Length - 4,
                4);

            var sut = this.Resolve<Processor>();

            using (var stream = new MemoryStream()) {
                sut.ProcessInternal(stream, 0x5678, 0x1234, 0x0483);

                var actual = stream.ToArray();

                Assert.That(actual, Is.EqualTo(expected));
            }
        }
    }
}
