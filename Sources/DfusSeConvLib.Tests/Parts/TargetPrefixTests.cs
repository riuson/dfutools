using Castle.Components.DictionaryAdapter;
using DfuSeConvLib.Extensions;
using DfuSeConvLib.Interfaces;
using DfuSeConvLib.Parts;
using Moq;
using NUnit.Framework;
using System.IO;

namespace DfuSeConvLib.Tests.Parts {
    [TestFixture]
    public class TargetPrefixTests {
        [Test]
        public void CanWrite() {
            var expected = new byte[] {
                // Signature.
                48, 49, 50, 51, 52, 53,
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
                0, 0, 0, 0,
                // NbElements.
                0, 0, 0, 0
            };

            var sut = new TargetPrefix();
            sut.Signature = "0123456789";
            sut.AlternateSetting = 1;
            sut.TargetNamed = true;
            sut.TargetName = "01234567890123456789";

            var dfuImagMock = new Mock<IDfuImage>();
            dfuImagMock.SetupGet(x => x.ImageElements).Returns(new EditableList<IImageElement>());
            var tempStream = new MemoryStream();
            sut.Write(tempStream, dfuImagMock.Object);

            var actual = tempStream.ToArray();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
