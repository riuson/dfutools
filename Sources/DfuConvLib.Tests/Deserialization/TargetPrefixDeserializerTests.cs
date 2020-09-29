using DfuConvLib.Deserialization;
using DfuConvLib.Exceptions;
using DfuConvLib.Interfaces;
using Moq;
using NUnit.Framework;
using System.IO;

namespace DfuConvLib.Tests.Deserialization {
    [TestFixture]
    public class TargetPrefixDeserializerTests {
        [Test]
        public void CanRead() {
            var sample = new byte[] {
                // Signature.
                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                // Alternate setting.
                1,
                // Target named.
                1, 0, 0, 0,
                // Target name.
                48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                // Target size.
                0x12, 0, 0, 0,
                // NbElements.
                0x34, 0, 0, 0
            };

            var tempStream = new MemoryStream(sample, false);

            var targetPrefixMock = new Mock<ITargetPrefix>();
            targetPrefixMock.SetupProperty(x => x.Signature);
            targetPrefixMock.SetupProperty(x => x.AlternateSetting);
            targetPrefixMock.SetupProperty(x => x.TargetNamed);
            targetPrefixMock.SetupProperty(x => x.TargetName);
            targetPrefixMock.SetupProperty(x => x.TargetSize);
            targetPrefixMock.SetupProperty(x => x.NbElements);

            var sut = new TargetPrefixDeserializer(
                (m, p) => new DeserializerException(m, p),
                () => targetPrefixMock.Object);

            var targetPrefix = sut.Read(tempStream);

            Assert.That(targetPrefix.Signature, Is.EqualTo("Target"));
            Assert.That(targetPrefix.AlternateSetting, Is.EqualTo(1));
            Assert.That(targetPrefix.TargetNamed, Is.True);
            Assert.That(targetPrefix.TargetName, Is.EqualTo("01234567890123456789"));
            Assert.That(targetPrefix.TargetSize, Is.EqualTo(0x12u));
            Assert.That(targetPrefix.NbElements, Is.EqualTo(0x34u));
        }

        [Test]
        public void CanReadWithEmptyName() {
            var sample = new byte[] {
                // Signature.
                0x54, 0x61, 0x72, 0x67, 0x65, 0x74,
                // Alternate setting.
                1,
                // Target named.
                0, 0, 0, 0,
                // Target name.
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                // Target size.
                0x12, 0, 0, 0,
                // NbElements.
                0x34, 0, 0, 0
            };

            var tempStream = new MemoryStream(sample, false);

            var targetPrefixMock = new Mock<ITargetPrefix>();
            targetPrefixMock.SetupProperty(x => x.Signature);
            targetPrefixMock.SetupProperty(x => x.AlternateSetting);
            targetPrefixMock.SetupProperty(x => x.TargetNamed);
            targetPrefixMock.SetupProperty(x => x.TargetName);
            targetPrefixMock.SetupProperty(x => x.TargetSize);
            targetPrefixMock.SetupProperty(x => x.NbElements);

            var sut = new TargetPrefixDeserializer(
                (m, p) => new DeserializerException(m, p),
                () => targetPrefixMock.Object);

            var targetPrefix = sut.Read(tempStream);

            Assert.That(targetPrefix.Signature, Is.EqualTo("Target"));
            Assert.That(targetPrefix.AlternateSetting, Is.EqualTo(1));
            Assert.That(targetPrefix.TargetNamed, Is.False);
            Assert.That(targetPrefix.TargetName, Is.EqualTo(string.Empty));
            Assert.That(targetPrefix.TargetSize, Is.EqualTo(0x12u));
            Assert.That(targetPrefix.NbElements, Is.EqualTo(0x34u));
        }
    }
}
