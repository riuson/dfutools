using DfuConvLib.Interfaces;
using System;
using System.IO;

namespace DfuConvLib.Deserialization {
    internal class DfuDeserializer : IDfuDeserializer {
        private readonly Func<IDfu> _createDfu;
        private readonly Func<IDfuImagesDeserializer> _createDfuImagesDeserializer;
        private readonly Func<IDfuPrefixDeserializer> _createDfuPrefixDeserializer;
        private readonly Func<IDfuSuffixDeserializer> _createDfuSuffixDeserializer;

        public DfuDeserializer(
            Func<IDfu> createDfu,
            Func<IDfuPrefixDeserializer> createDfuPrefixDeserializer,
            Func<IDfuImagesDeserializer> createDfuImagesDeserializer,
            Func<IDfuSuffixDeserializer> createDfuSuffixDeserializer) {
            this._createDfu = createDfu;
            this._createDfuPrefixDeserializer = createDfuPrefixDeserializer;
            this._createDfuImagesDeserializer = createDfuImagesDeserializer;
            this._createDfuSuffixDeserializer = createDfuSuffixDeserializer;
        }

        public IDfu Read(Stream stream) {
            var dfuPrefixDeserializer = this._createDfuPrefixDeserializer();
            var dfuPrefix = dfuPrefixDeserializer.Read(stream);

            var dfuImagesDeserializer = this._createDfuImagesDeserializer();
            var dfuImages = dfuImagesDeserializer.Read(stream, dfuPrefix.Targets);

            var dfuSuffixDeserializer = this._createDfuSuffixDeserializer();
            var dfuSuffix = dfuSuffixDeserializer.Read(stream);

            var dfu = this._createDfu();
            dfu.Prefix = dfuPrefix;
            dfu.Images = dfuImages;
            dfu.Suffix = dfuSuffix;
            return dfu;
        }
    }
}
