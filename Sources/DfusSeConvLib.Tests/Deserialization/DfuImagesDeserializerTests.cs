using DfuSeConvLib.Deserialization;
using DfuSeConvLib.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace DfuSeConvLib.Tests.Deserialization {
    [TestFixture]
    public class DfuImagesDeserializerTests {
        [Test]
        public void CanRead() {
            var streamMock = new Mock<Stream>();

            var list = new List<IDfuImage>();
            var dfuImagesMock = new Mock<IDfuImages>();
            dfuImagesMock.SetupGet(x => x.Images).Returns(list);

            var dfuImageMock = new Mock<IDfuImage>();

            var imagesCounter = 0;
            var dfuImageDeserializerMock = new Mock<IDfuImageDeserializer>();
            dfuImageDeserializerMock.Setup(x => x.Read(It.IsAny<Stream>()))
                .Callback<Stream>(s => imagesCounter++)
                .Returns(dfuImageMock.Object);

            var sut = new DfuImagesDeserializer(
                () => dfuImagesMock.Object,
                () => dfuImageDeserializerMock.Object);

            var dfuImages = sut.Read(streamMock.Object, 3);

            Assert.That(dfuImages.Images.Count, Is.EqualTo(3));
            Assert.That(imagesCounter, Is.EqualTo(3));
        }
    }
}
