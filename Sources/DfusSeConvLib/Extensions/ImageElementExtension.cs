using DfuSeConvLib.Parts;
using System;
using System.IO;
using System.Text;

namespace DfuSeConvLib.Extensions {
    public static class ImageElementExtension {
        public static uint CalculateSize(this ImageElement imageElement) =>
            Convert.ToUInt32(4 + 4 + imageElement.Data.Length);

        public static void Write(this ImageElement imageElement, Stream stream) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(imageElement.ElementAddress);
                writer.Write(Convert.ToUInt32(imageElement.Data.Length));
                writer.Write(imageElement.Data);
            }
        }
    }
}
