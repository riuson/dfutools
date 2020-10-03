using DfuLib.Helpers;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace DfuLib.Tests.Helpers {
    [TestFixture]
    public class StreamStringTests {
        [Test]
        public void CanReadEmpty() {
            var sample = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

            using (var stream = new MemoryStream(sample, false)) {
                using (var reader = new BinaryReader(stream, Encoding.ASCII)) {
                    var actual = reader.ReadString(8);

                    Assert.That(actual, Is.Empty);
                }
            }
        }

        [Test]
        public void CanReadFull() {
            var sample = new byte[] { 48, 49, 50, 51, 52, 53, 54, 55 };

            using (var stream = new MemoryStream(sample, false)) {
                using (var reader = new BinaryReader(stream, Encoding.ASCII)) {
                    var actual = reader.ReadString(8);

                    Assert.That(actual, Is.EqualTo("01234567"));
                }
            }
        }

        [Test]
        public void CanReadPartial() {
            var sample = new byte[] { 48, 49, 50, 51, 0, 0, 0, 0 };

            using (var stream = new MemoryStream(sample, false)) {
                using (var reader = new BinaryReader(stream, Encoding.ASCII)) {
                    var actual = reader.ReadString(8);

                    Assert.That(actual, Is.EqualTo("0123"));
                }
            }
        }

        [Test]
        public void CanReadPartialWithGarbage() {
            var sample = new byte[] { 48, 49, 50, 51, 0, 0, 52, 0 };

            using (var stream = new MemoryStream(sample, false)) {
                using (var reader = new BinaryReader(stream, Encoding.ASCII)) {
                    var actual = reader.ReadString(8);

                    Assert.That(actual, Is.EqualTo("0123"));
                }
            }
        }

        [Test]
        public void CanWriteEmpty() {
            var expected = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 1 };
            var actual = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 1 };

            using (var stream = new MemoryStream(actual, true)) {
                using (var writer = new BinaryWriter(stream, Encoding.ASCII)) {
                    writer.WriteString(string.Empty, 8);

                    Assert.That(actual, Is.EqualTo(expected));
                }
            }
        }

        [Test]
        public void CanWriteFull() {
            var expected = new byte[] { 48, 49, 50, 51, 52, 53, 54, 55, 1 };
            var actual = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 1 };

            using (var stream = new MemoryStream(actual, true)) {
                using (var writer = new BinaryWriter(stream, Encoding.ASCII)) {
                    writer.WriteString("01234567", 8);

                    Assert.That(actual, Is.EqualTo(expected));
                }
            }
        }

        [Test]
        public void CanWritePartial() {
            var expected = new byte[] { 48, 49, 50, 51, 0, 0, 0, 0, 1 };
            var actual = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 1 };

            using (var stream = new MemoryStream(actual, true)) {
                using (var writer = new BinaryWriter(stream, Encoding.ASCII)) {
                    writer.WriteString("0123", 8);

                    Assert.That(actual, Is.EqualTo(expected));
                }
            }
        }
    }
}
