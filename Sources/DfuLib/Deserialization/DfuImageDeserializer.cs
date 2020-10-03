using DfuLib.Interfaces;
using System;
using System.IO;

namespace DfuLib.Deserialization {
    internal class DfuImageDeserializer : IDfuImageDeserializer {
        private readonly Func<IDfuImage> _createDfuImage;
        private readonly Func<IImageElementDeserializer> _createImageElementDeserializer;
        private readonly Func<ITargetPrefixDeserializer> _createTargetPrefixDeserializer;

        public DfuImageDeserializer(
            Func<ITargetPrefixDeserializer> createTargetPrefixDeserializer,
            Func<IImageElementDeserializer> createImageElementDeserializer,
            Func<IDfuImage> createDfuImage) {
            this._createTargetPrefixDeserializer = createTargetPrefixDeserializer;
            this._createImageElementDeserializer = createImageElementDeserializer;
            this._createDfuImage = createDfuImage;
        }

        public IDfuImage Read(Stream stream) {
            var targetPrefixDeserializer = this._createTargetPrefixDeserializer();
            var targetPrefix = targetPrefixDeserializer.Read(stream);

            var imageElementDeserializer = this._createImageElementDeserializer();

            var dfuImage = this._createDfuImage();
            dfuImage.Prefix = targetPrefix;

            for (var i = 0; i < targetPrefix.NbElements; i++) {
                var imageElement = imageElementDeserializer.Read(stream);
                dfuImage.ImageElements.Add(imageElement);
            }

            return dfuImage;
        }
    }
}
