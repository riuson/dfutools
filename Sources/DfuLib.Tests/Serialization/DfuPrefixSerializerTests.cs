using Castle.Components.DictionaryAdapter;
using DfuLib.Interfaces;
using DfuLib.Serialization;
using Moq;
using NUnit.Framework;
using System.IO;

namespace DfuLib.Tests.Serialization {
    [TestFixture]
    public class DfuPrefixSerializerTests {
        [Test]
        public void CanWrite() {
            var expected = new byte[] {
                0x44, 0x66, 0x75, 0x53, 0x65,
                0x01,
                0x0b, 0x00, 0x00, 0x00,
                0x00
            };

            var dfuImagesMock = new Mock<IDfuImages>();
            dfuImagesMock.SetupGet(x => x.Images).Returns(new EditableList<IDfuImage>());

            var dfuPrefixMock = new Mock<IDfuPrefix>();
            dfuPrefixMock.SetupGet(x => x.Signature).Returns("DfuSe");
            dfuPrefixMock.SetupGet(x => x.Version).Returns(1);

            var dfuImagesSerialzerMock = new Mock<IDfuImagesSerializer>();

            var sut = new DfuPrefixSerializer(
                () => new DfuImagesSerializer(
                    () => new DfuImageSerializer(
                        () => new TargetPrefixSerializer(),
                        () => new ImageElementSerializer())));

            var tempStream = new MemoryStream();
            sut.Write(tempStream, dfuPrefixMock.Object, dfuImagesMock.Object);

            var actual = tempStream.ToArray();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
