using Castle.Components.DictionaryAdapter;
using DfuSeConvLib.Interfaces;
using DfuSeConvLib.Serialization;
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

            var targetPrefixMock = new Mock<ITargetPrefix>();
            targetPrefixMock.SetupGet(x => x.Signature).Returns("0123456789");
            targetPrefixMock.SetupGet(x => x.AlternateSetting).Returns(1);
            targetPrefixMock.SetupGet(x => x.TargetNamed).Returns(true);
            targetPrefixMock.SetupGet(x => x.TargetName).Returns("01234567890123456789");

            var dfuImageMock = new Mock<IDfuImage>();
            dfuImageMock.SetupGet(x => x.ImageElements).Returns(new EditableList<IImageElement>());
            var tempStream = new MemoryStream();

            var sut = new TargetPrefixSerializer();

            sut.Write(tempStream, targetPrefixMock.Object, dfuImageMock.Object.ImageElements);

            var actual = tempStream.ToArray();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
