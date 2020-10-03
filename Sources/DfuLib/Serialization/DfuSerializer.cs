using DfuLib.Interfaces;
using System;
using System.IO;

namespace DfuLib.Serialization {
    internal class DfuSerializer : IDfuSerializer {
        private readonly Func<IDfuImagesSerializer> _createDfuImagesSerializer;
        private readonly Func<IDfuPrefixSerializer> _createDfuPrefixSerializer;
        private readonly Func<IDfuSuffixSerializer> _createDfuSuffixSerializer;

        public DfuSerializer(
            Func<IDfuPrefixSerializer> createDfuPrefixSerializer,
            Func<IDfuImagesSerializer> createDfuImagesSerializer,
            Func<IDfuSuffixSerializer> createDfuSuffixSerializer) {
            this._createDfuPrefixSerializer = createDfuPrefixSerializer;
            this._createDfuImagesSerializer = createDfuImagesSerializer;
            this._createDfuSuffixSerializer = createDfuSuffixSerializer;
        }

        public uint GetSize(IDfu dfu) {
            var dfuPrefixSerializer = this._createDfuPrefixSerializer();
            var dfuImagesSerializer = this._createDfuImagesSerializer();
            var dfuSuffixSerializer = this._createDfuSuffixSerializer();

            return dfuPrefixSerializer.GetSize() +
                   dfuImagesSerializer.GetSize(dfu.Images) +
                   dfuSuffixSerializer.GetSize();
        }

        public void Write(Stream stream, IDfu dfu) {
            var dfuPrefixSerializer = this._createDfuPrefixSerializer();
            dfuPrefixSerializer.Write(stream, dfu.Prefix, dfu.Images);

            var dfuImagesSerializer = this._createDfuImagesSerializer();
            dfuImagesSerializer.Write(stream, dfu.Images);

            var dfuSuffixSerializer = this._createDfuSuffixSerializer();
            dfuSuffixSerializer.Write(stream, dfu.Suffix);
        }
    }
}
