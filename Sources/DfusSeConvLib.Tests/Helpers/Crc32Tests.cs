using DfuSeConvLib.Helpers;
using NUnit.Framework;
using System;
using System.Linq;

namespace DfuSeConvLib.Tests.Helpers {
    [TestFixture]
    public class Crc32Tests {
        [Test]
        public void CanComputByParts() {
            var sample = new byte[] {
                // 0 ... 16
                0x44, 0x66, 0x75, 0x53, 0x65, 0x01, 0x9F, 0xD4, 0x00, 0x00, 0x02, 0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                // 17 ... 33
                0x01, 0x01, 0x00, 0x00, 0x00, 0x54, 0x41, 0x52, 0x47, 0x4E, 0x41, 0x4D, 0x45, 0x00, 0x72, 0x6F, 0x6A,
                // 34 ... 50
                0x65, 0x63, 0x74, 0x73, 0x5C, 0x44, 0x65, 0x73, 0x6B, 0x74, 0x6F, 0x70, 0x5C, 0x44, 0x66, 0x75, 0x53,
                // 51
                0x65
            };

            var expected = 0x5082a218u;

            var temp = Crc32.Init();
            temp = Crc32.ComputePart(
                temp,
                sample.Skip(0).Take(17).ToArray(),
                0,
                17);
            temp = Crc32.ComputePart(
                temp,
                sample.Skip(17).Take(17).ToArray(),
                0,
                17);
            temp = Crc32.ComputePart(
                temp,
                sample.Skip(17 + 17).Take(17).ToArray(),
                0,
                17);
            temp = Crc32.ComputePart(
                temp,
                sample.Skip(17 + 17 + 17).Take(1).ToArray(),
                0,
                1);
            var actual = Crc32.Finish(temp);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void CanComputeAtOnce() {
            var sample = new byte[] {
                0x44, 0x66, 0x75, 0x53, 0x65, 0x01, 0x9F, 0xD4, 0x00, 0x00, 0x02, 0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                0x01, 0x01, 0x00, 0x00, 0x00, 0x54, 0x41, 0x52, 0x47, 0x4E, 0x41, 0x4D, 0x45, 0x00, 0x72, 0x6F, 0x6A,
                0x65, 0x63, 0x74, 0x73, 0x5C, 0x44, 0x65, 0x73, 0x6B, 0x74, 0x6F, 0x70, 0x5C, 0x44, 0x66, 0x75, 0x53,
                0x65
            };

            var expected = 0x5082a218u;
            var actual = Crc32.ComputeAll(sample, 0, Convert.ToUInt32(sample.Length));

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
