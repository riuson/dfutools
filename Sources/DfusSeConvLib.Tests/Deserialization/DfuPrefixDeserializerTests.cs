using DfuSeConvLib.Deserialization;
using DfuSeConvLib.Exceptions;
using DfuSeConvLib.Interfaces;
using Moq;
using NUnit.Framework;
using System.IO;

namespace DfuSeConvLib.Tests.Deserialization {
    [TestFixture]
    public class DfuPrefixDeserializerTests {
        [Test]
        public void CanRead() {
            var sample = new byte[] {
                0x44, 0x66, 0x75, 0x53, 0x65,
                0x01,
                0x19, 0x00, 0x00, 0x00,
                0x00,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0
            };

            var dfuPrefixMock = new Mock<IDfuPrefix>();
            dfuPrefixMock.SetupProperty(x => x.Signature);
            dfuPrefixMock.SetupProperty(x => x.Version);
            dfuPrefixMock.SetupProperty(x => x.DfuImageSize);
            dfuPrefixMock.SetupProperty(x => x.Targets);

            var sut = new DfuPrefixDeserializer(
                (m, p) => new DeserializerException(m, p),
                () => dfuPrefixMock.Object);

            var tempStream = new MemoryStream(sample, false);
            var dfuPrefix = sut.Read(tempStream);

            Assert.That(dfuPrefix.Signature, Is.EqualTo("DfuSe"));
            Assert.That(dfuPrefix.Version, Is.EqualTo(1));
            Assert.That(dfuPrefix.DfuImageSize, Is.EqualTo(11u + 30u - 16u));
            Assert.That(dfuPrefix.Targets, Is.EqualTo(0u));
        }
    }
}
