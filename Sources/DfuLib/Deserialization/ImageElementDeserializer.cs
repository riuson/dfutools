using DfuLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuLib.Deserialization {
    internal class ImageElementDeserializer : IImageElementDeserializer {
        private readonly Func<IImageElement> _createImageElement;

        public ImageElementDeserializer(Func<IImageElement> createImageElement) =>
            this._createImageElement = createImageElement;

        public IImageElement Read(Stream stream) {
            using (var reader = new BinaryReader(stream, Encoding.ASCII, true)) {
                var imageElement = this._createImageElement();
                imageElement.ElementAddress = reader.ReadUInt32();
                imageElement.ElementSize = reader.ReadUInt32();
                var buffer = new byte[imageElement.ElementSize];
                reader.Read(buffer, 0, Convert.ToInt32(imageElement.ElementSize));
                imageElement.Data = buffer;
                return imageElement;
            }
        }
    }
}
