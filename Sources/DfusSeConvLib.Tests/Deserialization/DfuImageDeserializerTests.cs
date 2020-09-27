using DfuSeConvLib.Deserialization;
using DfuSeConvLib.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace DfuSeConvLib.Tests.Deserialization {
    [TestFixture]
    public class DfuImageDeserializerTests {
        [Test]
        public void CanRead() {
            var streamMock = new Mock<Stream>();

            var targetPrefixMock = new Mock<ITargetPrefix>();
            targetPrefixMock.SetupProperty(x => x.NbElements, 2u);

            var targetPrefixDeserializerMock = new Mock<ITargetPrefixDeserializer>();
            targetPrefixDeserializerMock.Setup(x => x.Read(It.IsAny<Stream>()))
                .Returns(targetPrefixMock.Object);

            var imageElementMock = new Mock<IImageElement>();

            var imagesCount = 0;
            var imageElementDeserializerMock = new Mock<IImageElementDeserializer>();
            imageElementDeserializerMock.Setup(x => x.Read(It.IsAny<Stream>()))
                .Callback<Stream>(s => imagesCount++)
                .Returns(imageElementMock.Object);

            var dfuImageMock = new Mock<IDfuImage>();
            dfuImageMock.SetupProperty(x => x.Prefix);
            var list = new List<IImageElement>();
            dfuImageMock.SetupGet(x => x.ImageElements).Returns(list);

            var sut = new DfuImageDeserializer(
                () => targetPrefixDeserializerMock.Object,
                () => imageElementDeserializerMock.Object,
                () => dfuImageMock.Object);

            var dfuImage = sut.Read(streamMock.Object);

            Assert.That(dfuImage.Prefix.NbElements, Is.EqualTo(2u));
            Assert.That(imagesCount, Is.EqualTo(2));
        }
    }
}
