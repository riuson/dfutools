using DfuSeConvLib.Parts;
using System;
using System.IO;
using System.Linq;

namespace DfuSeConvLib.Extensions {
    public static class DfuImageExtension {
        public static uint CalculateSize(this DfuImage dfuImage) =>
            dfuImage.Prefix.CalculateSize() + Convert.ToUInt32(dfuImage.ImageElements.Sum(x => x.CalculateSize()));

        public static void Write(this DfuImage dfuImage, Stream stream) {
            dfuImage.Prefix.Write(stream, dfuImage);

            foreach (var dfuImageElement in dfuImage.ImageElements) {
                dfuImageElement.Write(stream);
            }
        }
    }
}
