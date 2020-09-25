using DfuSeConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuSeConvLib.Serialization {
    internal class ImageElementSerializer : IImageElementSerializer {
        public uint GetSize(IImageElement imageElement) => imageElement.ElementSize + 8;

        public void Write(Stream stream, IImageElement imageElement) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(imageElement.ElementAddress);
                writer.Write(Convert.ToUInt32(imageElement.Data.Length));
                writer.Write(imageElement.Data);
            }
        }
    }
}
