﻿using DfuLib.Interfaces;
using DfuLib.Parts;
using NUnit.Framework;
using System.IO;

namespace DfuLib.Tests.Serialization {
    [TestFixture]
    public class DfuSerializerIoCTests : IoCSupportedTest<DfuLogicModule> {
        [OneTimeTearDown]
        public void TearDown() {
            this.ShutdownIoC();
        }

        [Test]
        public void CanWriteReal() {
            var sampleData = new byte[] { 1, 2, 3, 4 };

            var dfuPrefix = new DfuPrefix();

            var dfuSuffix = new DfuSuffix {
                Device = 0x5678,
                Product = 0x1234,
                Vendor = 0x0483
            };

            var targetPrefix = new TargetPrefix {
                TargetId = 2,
                IsTargetNamed = true,
                TargetName = new string('Q', 256)
            };

            var imageElement = new ImageElement {
                ElementAddress = 0x08000000,
                Data = sampleData
            };

            var dfuImage = new DfuImage {
                Prefix = targetPrefix
            };
            dfuImage.ImageElements.Add(imageElement);

            var dfuImages = new DfuImages();
            dfuImages.Images.Add(dfuImage);


            var dfu = new Dfu {
                Prefix = dfuPrefix,
                Images = dfuImages,
                Suffix = dfuSuffix
            };

            // Array generated by DFU File Manager v3.0.6
            var expected = new byte[] {
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

            var sut = this.Resolve<IDfuSerializer>();

            var tempStream = new MemoryStream();
            sut.Write(tempStream, dfu);

            var actual = tempStream.ToArray();

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
