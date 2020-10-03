using DfuLib.Interfaces;
using System;
using System.IO;

namespace DfuLib.Serialization {
    internal class DfuImageSerializer : IDfuImageSerializer {
        private readonly Func<IImageElementSerializer> _createImageElementSerializer;
        private readonly Func<ITargetPrefixSerializer> _createTargetPrefixSerializer;

        public DfuImageSerializer(
            Func<ITargetPrefixSerializer> createTargetPrefixSerializer,
            Func<IImageElementSerializer> createImageElementSerializer) {
            this._createTargetPrefixSerializer = createTargetPrefixSerializer;
            this._createImageElementSerializer = createImageElementSerializer;
        }

        public uint GetSize(IDfuImage dfuImage) {
            var targetPrefixSerializer =
                this._createTargetPrefixSerializer();
            var result = targetPrefixSerializer.GetSize();

            foreach (var imageElement in dfuImage.ImageElements) {
                var imageElementSerializer = this._createImageElementSerializer();
                result += imageElementSerializer.GetSize(imageElement);
            }

            return result;
        }

        public void Write(Stream stream, IDfuImage dfuImage) {
            var targetPrefixSerializer =
                this._createTargetPrefixSerializer();
            targetPrefixSerializer.Write(stream, dfuImage.Prefix, dfuImage.ImageElements);

            foreach (var dfuImageElement in dfuImage.ImageElements) {
                var imageElementSerializer = this._createImageElementSerializer();
                imageElementSerializer.Write(stream, dfuImageElement);
            }
        }
    }
}
