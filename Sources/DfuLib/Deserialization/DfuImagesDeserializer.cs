using DfuLib.Interfaces;
using System;
using System.IO;

namespace DfuLib.Deserialization {
    internal class DfuImagesDeserializer : IDfuImagesDeserializer {
        private readonly Func<IDfuImageDeserializer> _createDfuImageDeserializer;
        private readonly Func<IDfuImages> _createDfuImages;

        public DfuImagesDeserializer(
            Func<IDfuImages> createDfuImages,
            Func<IDfuImageDeserializer> createDfuImageDeserializer) {
            this._createDfuImages = createDfuImages;
            this._createDfuImageDeserializer = createDfuImageDeserializer;
        }

        public IDfuImages Read(Stream stream, int targets) {
            var dfuImages = this._createDfuImages();
            var dfuImageDeserializer = this._createDfuImageDeserializer();

            for (var i = 0; i < targets; i++) {
                var dfuImage = dfuImageDeserializer.Read(stream);
                dfuImages.Images.Add(dfuImage);
            }

            return dfuImages;
        }
    }
}
