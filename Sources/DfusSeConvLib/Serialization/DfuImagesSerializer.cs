using DfuSeConvLib.Interfaces;
using System;
using System.IO;

namespace DfuSeConvLib.Serialization {
    internal class DfuImagesSerializer : ISerializer {
        private readonly Func<IDfuImage, ISerializer> _createDfuImageSerializer;
        private readonly IDfuImages _dfuImages;

        public DfuImagesSerializer(
            IDfuImages dfuImages,
            Func<IDfuImage, ISerializer> createDfuImageSerializer) {
            this._dfuImages = dfuImages;
            this._createDfuImageSerializer = createDfuImageSerializer;
        }

        public uint Size {
            get {
                uint result = 0;

                foreach (var image in this._dfuImages.Images) {
                    var dfuImageSerializer = this._createDfuImageSerializer(image);
                    result += dfuImageSerializer.Size;
                }

                return result;
            }
        }

        public void Write(Stream stream) {
            foreach (var image in this._dfuImages.Images) {
                var dfuImageSerializer = this._createDfuImageSerializer(image);
                dfuImageSerializer.Write(stream);
            }
        }
    }
}
