using DfuSeConvLib.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DfuSeConvLib.Extensions {
    public static class DfuImageExtension {
        public static uint CalculateSize(this IDfuImage dfuImage) =>
            dfuImage.Prefix.CalculateSize() +
            Convert.ToUInt32((int) dfuImage.ImageElements.Sum(x => x.CalculateSize()));

        public static void Write(this IDfuImage dfuImage, Stream stream) {
            dfuImage.Prefix.Write(stream, dfuImage);

            foreach (var dfuImageElement in dfuImage.ImageElements) {
                dfuImageElement.Write(stream);
            }
        }
    }
}
