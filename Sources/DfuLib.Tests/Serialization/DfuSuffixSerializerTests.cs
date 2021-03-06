﻿using DfuLib.Interfaces;
using DfuLib.Serialization;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace DfuLib.Tests.Serialization {
    [TestFixture]
    public class DfuSuffixSerializerTests {
        [Test]
        public void CanWrite() {
            // Array generated by DFU File Manager v3.0.6
            var sample = new byte[] {
                0x44, 0x66, 0x75, 0x53, 0x65,
                0x01,
                0x29, 0x01, 0x00, 0x00,
                0x01,

                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                0x02,
                0x01, 0x00, 0x00, 0x00,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51, 0x51,
                0x0C, 0x00, 0x00, 0x00,
                0x01, 0x00, 0x00, 0x00,

                0x00, 0x00, 0x00, 0x08,
                0x04, 0x00, 0x00, 0x00,
                0x01, 0x02, 0x03, 0x04,

                0x78, 0x56,
                0x34, 0x12,
                0x83, 0x04,
                0x1A, 0x01,
                0x55, 0x46, 0x44,
                0x10,
                0xB7, 0x02, 0xE0, 0x8D
            };

            var dfuSuffixMock = new Mock<IDfuSuffix>();
            dfuSuffixMock.SetupGet(x => x.Device).Returns(0x5678);
            dfuSuffixMock.SetupGet(x => x.Product).Returns(0x1234);
            dfuSuffixMock.SetupGet(x => x.Vendor).Returns(0x0483);
            dfuSuffixMock.SetupGet(x => x.Dfu).Returns(0x011a);
            dfuSuffixMock.SetupGet(x => x.Length).Returns(16);
            dfuSuffixMock.SetupGet(x => x.DfuSignature).Returns("UFD");


            var tempStream = new MemoryStream();
            tempStream.Write(sample, 0, sample.Length - 16);

            var sut = new DfuSuffixSerializer();
            sut.Write(tempStream, dfuSuffixMock.Object);

            var array = tempStream.ToArray();
            var actual = array.Skip(sample.Length - 16).Take(16).ToArray();
            var expected = sample.Skip(sample.Length - 16).Take(16).ToArray();

            Assert.That(array.Length, Is.EqualTo(sample.Length));
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
