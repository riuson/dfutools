using Castle.Components.DictionaryAdapter;
using DfuConvLib.Interfaces;
using DfuConvLib.Serialization;
using Moq;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace DfuConvLib.Tests.Serialization {
    [TestFixture]
    public class DfuImagesSerializerTests {
        [Test]
        public void CanWrite() {
            var image1Mock = new Mock<IDfuImage>();
            var image2Mock = new Mock<IDfuImage>();

            var dfuImagesMock = new Mock<IDfuImages>();
            dfuImagesMock.SetupGet(x => x.Images)
                .Returns(new EditableList<IDfuImage> { image1Mock.Object, image2Mock.Object });

            byte b = 0;

            var dfuSerializerMock = new Mock<IDfuImageSerializer>();
            dfuSerializerMock.Setup(x =>
                    x.Write(It.IsAny<Stream>(), It.IsAny<IDfuImage>()))
                .Callback<Stream, IDfuImage>((x, d) => {
                    var array = new[] { b++, b++, b++, b++ };
                    x.Write(array, 0, 4);
                });

            var sut = new DfuImagesSerializer(
                () => dfuSerializerMock.Object);

            var tempStream = new MemoryStream();
            sut.Write(tempStream, dfuImagesMock.Object);

            var actual = tempStream.ToArray();
            var expected = Enumerable.Range(0, 8).Select(x => (byte) x).ToArray();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
