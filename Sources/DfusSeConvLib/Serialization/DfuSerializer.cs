using DfuSeConvLib.Interfaces;
using System;
using System.IO;

namespace DfuSeConvLib.Serialization {
    internal class DfuSerializer : ISerializer {
        private readonly Func<IDfuImages, ISerializer> _createDfuImagesSerializer;
        private readonly Func<IDfuPrefix, ISerializer> _createDfuPrefixSerializer;
        private readonly Func<IDfuSuffix, ISerializer> _createDfuSuffixSerializer;
        private readonly IDfu _dfu;

        public DfuSerializer(
            IDfu dfu,
            Func<IDfuPrefix, ISerializer> createDfuPrefixSerializer,
            Func<IDfuImages, ISerializer> createDfuImagesSerializer,
            Func<IDfuSuffix, ISerializer> createDfuSuffixSerializer) {
            this._dfu = dfu;
            this._createDfuPrefixSerializer = createDfuPrefixSerializer;
            this._createDfuImagesSerializer = createDfuImagesSerializer;
            this._createDfuSuffixSerializer = createDfuSuffixSerializer;
        }

        public uint Size {
            get {
                var dfuPrefixSerializer = this._createDfuPrefixSerializer(this._dfu.Prefix);
                var dfuImagesSerializer = this._createDfuImagesSerializer(this._dfu.Images);
                var dfuSuffixSerializer = this._createDfuSuffixSerializer(this._dfu.Suffix);

                return dfuPrefixSerializer.Size + dfuImagesSerializer.Size + dfuSuffixSerializer.Size;
            }
        }

        public void Write(Stream stream) {
            var dfuPrefixSerializer = this._createDfuPrefixSerializer(this._dfu.Prefix);
            dfuPrefixSerializer.Write(stream);

            var dfuImagesSerializer = this._createDfuImagesSerializer(this._dfu.Images);
            dfuImagesSerializer.Write(stream);

            var dfuSuffixSerializer = this._createDfuSuffixSerializer(this._dfu.Suffix);
            dfuSuffixSerializer.Write(stream);
        }
    }
}
