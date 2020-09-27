using DfuConvLib.Helpers;
using DfuConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuConvLib.Serialization {
    internal class DfuPrefixSerializer : IDfuPrefixSerializer {
        private readonly Func<IDfuImagesSerializer> _createDfuImagesSerializer;

        public DfuPrefixSerializer(
            Func<IDfuImagesSerializer> createDfuImagesSerializer) =>
            this._createDfuImagesSerializer = createDfuImagesSerializer;

        public uint GetSize() => 11;

        public void Write(
            Stream stream,
            IDfuPrefix dfuPrefix,
            IDfuImages dfuImages) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.WriteString(dfuPrefix.Signature, 5);
                writer.Write(Convert.ToByte(dfuPrefix.Version));

                var dfuImagesSerializer = this._createDfuImagesSerializer();
                var totalLength = 11 + dfuImagesSerializer.GetSize(dfuImages);

                writer.Write(totalLength);
                writer.Write(Convert.ToByte(dfuImages.Images.Count));
            }
        }
    }
}
