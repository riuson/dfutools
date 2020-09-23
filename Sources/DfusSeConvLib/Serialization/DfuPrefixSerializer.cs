using DfuSeConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuSeConvLib.Serialization {
    internal class DfuPrefixSerializer : ISerializer {
        private readonly Func<IDfuImage, ISerializer> _createDfuImageSerializer;
        private readonly IDfuImages _dfuImages;
        private readonly IDfuPrefix _dfuPrefix;

        public DfuPrefixSerializer(
            IDfuPrefix dfuPrefix,
            IDfuImages dfuImages,
            Func<IDfuImage, ISerializer> createDfuImageSerializer) {
            this._dfuPrefix = dfuPrefix;
            this._dfuImages = dfuImages;
            this._createDfuImageSerializer = createDfuImageSerializer;
        }

        public uint Size => 11;

        public void Write(Stream stream) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(this._dfuPrefix.Signature.PadRight(5, '\x0000').ToCharArray(), 0, 5);
                writer.Write(Convert.ToByte(this._dfuPrefix.Version));

                uint totalLength = 11;

                foreach (var image in this._dfuImages.Images) {
                    var dfuImageSerializer = this._createDfuImageSerializer(image);
                    totalLength += dfuImageSerializer.Size;
                }

                writer.Write(totalLength);
                writer.Write(Convert.ToByte(this._dfuImages.Images.Count));
            }
        }
    }
}
