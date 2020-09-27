using DfuConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuConvLib.Serialization {
    internal class ImageElementSerializer : IImageElementSerializer {
        public uint GetSize(IImageElement imageElement) => Convert.ToUInt32(imageElement.Data.Length) + 8;

        public void Write(Stream stream, IImageElement imageElement) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(imageElement.ElementAddress);
                writer.Write(Convert.ToUInt32(imageElement.Data.Length));
                writer.Write(imageElement.Data);
            }
        }
    }
}
