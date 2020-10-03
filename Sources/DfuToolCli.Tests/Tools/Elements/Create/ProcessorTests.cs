﻿using DfuLib.Helpers;
using DfuToolCli.Tests.Helpers;
using DfuToolCli.Tools.Elements.Create;
using NUnit.Framework;
using System;
using System.IO;

namespace DfuToolCli.Tests.Tools.Elements.Create {
    [TestFixture]
    public class ProcessorTests : BaseTestFixture {
        [Test]
        public void CanElementCreate() {
            // Sample.
            var sample = new byte[] {
                0x44, 0x66, 0x75, 0x53, 0x65,
                0x01,
                0x00, 0x00, 0x00, 0x00,
                0x01,

                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                0x01,
                0x01, 0x00, 0x00, 0x00,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00,

                0x78, 0x56,
                0x34, 0x12,
                0x83, 0x04,
                0x1A, 0x01,
                0x55, 0x46, 0x44,
                0x10,
                0x00, 0x00, 0x00, 0x00
            };

            var length = sample.Length - 16;
            Array.Copy(
                BitConverter.GetBytes(length),
                0,
                sample,
                6,
                4);

            var crc32 = Crc32.ComputeAll(sample, 0, Convert.ToUInt32(sample.Length - 4));
            Array.Copy(
                BitConverter.GetBytes(crc32 ^ 0xffffffffu),
                0,
                sample,
                sample.Length - 4,
                4);


            var elementData = new byte[] {
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f
            };
            var elementAddress = 0x08000000u;

            // Expected.
            var expected = new byte[] {
                0x44, 0x66, 0x75, 0x53, 0x65,
                0x01,
                0x00, 0x00, 0x00, 0x00,
                0x01,

                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                0x01,
                0x01, 0x00, 0x00, 0x00,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x54, 0x54, 0x54, 0x54, 0x54, 0x54, 0x54,
                0x18, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x08,
                0x10, 0x00, 0x00, 0x00,
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07,
                0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f,

                0x78, 0x56,
                0x34, 0x12,
                0x83, 0x04,
                0x1A, 0x01,
                0x55, 0x46, 0x44,
                0x10,
                0x00, 0x00, 0x00, 0x00
            };

            length = expected.Length - 16;
            expected.WriteInteger(6, Convert.ToUInt32(length));

            crc32 = Crc32.ComputeAll(expected, 0, Convert.ToUInt32(expected.Length - 4));
            expected.WriteInteger(expected.Length - 4, crc32 ^ 0xffffffffu);

            // Test.
            var sut = this.Resolve<Processor>();

            using (var dfuStream = new MemoryStream()) {
                dfuStream.Write(sample, 0, sample.Length);
                dfuStream.Seek(0, SeekOrigin.Begin);

                using (var elementStream = new MemoryStream(elementData, false)) {
                    sut.ProcessInternal(dfuStream, 1, elementAddress, elementStream);

                    var actual = dfuStream.ToArray();

                    Assert.That(actual, Is.EqualTo(expected));
                }
            }
        }
    }
}