using DfuConvLib.Interfaces;
using System;
using System.IO;

namespace DfuConvLib.Serialization {
    internal class DfuImagesSerializer : IDfuImagesSerializer {
        private readonly Func<IDfuImageSerializer> _createDfuImageSerializer;

        public DfuImagesSerializer(
            Func<IDfuImageSerializer> createDfuImageSerializer) =>
            this._createDfuImageSerializer = createDfuImageSerializer;

        public uint GetSize(IDfuImages dfuImages) {
            uint result = 0;

            foreach (var image in dfuImages.Images) {
                var dfuImageSerializer = this._createDfuImageSerializer();
                result += dfuImageSerializer.GetSize(image);
            }

            return result;
        }

        public void Write(Stream stream, IDfuImages dfuImages) {
            foreach (var image in dfuImages.Images) {
                var dfuImageSerializer = this._createDfuImageSerializer();
                dfuImageSerializer.Write(stream, image);
            }
        }
    }
}
