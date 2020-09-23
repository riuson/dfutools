using DfuSeConvLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;

namespace DfuSeConvLib.Serialization {
    internal class DfuImageSerializer : ISerializer {
        private readonly Func<IImageElement, ISerializer> _createImageElement;
        private readonly Func<ITargetPrefix, IEnumerable<IImageElement>, ISerializer> _createTargetPrefixSerializer;
        private readonly IDfuImage _dfuImage;

        public DfuImageSerializer(
            IDfuImage dfuImage,
            Func<ITargetPrefix, IEnumerable<IImageElement>, ISerializer> createTargetPrefixSerializer,
            Func<IImageElement, ISerializer> createImageElement) {
            this._dfuImage = dfuImage;
            this._createTargetPrefixSerializer = createTargetPrefixSerializer;
            this._createImageElement = createImageElement;
        }

        public uint Size {
            get {
                var targetPrefixSerializer =
                    this._createTargetPrefixSerializer(this._dfuImage.Prefix, this._dfuImage.ImageElements);
                var result = targetPrefixSerializer.Size;

                foreach (var dfuImageElement in this._dfuImage.ImageElements) {
                    var imageElementSerializer = this._createImageElement(dfuImageElement);
                    result += imageElementSerializer.Size;
                }

                return result;
            }
        }

        public void Write(Stream stream) {
            var targetPrefixSerializer =
                this._createTargetPrefixSerializer(this._dfuImage.Prefix, this._dfuImage.ImageElements);
            targetPrefixSerializer.Write(stream);

            foreach (var dfuImageElement in this._dfuImage.ImageElements) {
                var imageElementSerializer = this._createImageElement(dfuImageElement);
                imageElementSerializer.Write(stream);
            }
        }
    }
}
