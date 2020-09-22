using DfuSeConvLib.Parts;
using System;
using System.IO;
using System.Linq;

namespace DfuSeConvLib.Extensions {
    public static class DfuImagesExtension {
        public static uint CalculateSize(this DfuImages dfuImages) =>
            Convert.ToUInt32(dfuImages.Images.Sum(x => x.CalculateSize()));

        public static void Write(this DfuImages dfuImages, Stream stream) {
            foreach (var image in dfuImages.Images) {
                image.Write(stream);
            }
        }
    }
}
