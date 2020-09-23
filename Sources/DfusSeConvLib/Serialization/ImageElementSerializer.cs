using DfuSeConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuSeConvLib.Serialization {
    internal class ImageElementSerializer : ISerializer {
        private readonly IImageElement _imageElement;

        public ImageElementSerializer(IImageElement imageElement) => this._imageElement = imageElement;

        public uint Size => this._imageElement.ElementSize + 8;

        public void Write(Stream stream) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(this._imageElement.ElementAddress);
                writer.Write(Convert.ToUInt32(this._imageElement.Data.Length));
                writer.Write(this._imageElement.Data);
            }
        }
    }
}
