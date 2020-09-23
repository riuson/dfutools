using DfuSeConvLib.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DfuSeConvLib.Serialization {
    internal class TargetPrefixSerializer : ISerializer {
        private readonly IEnumerable<IImageElement> _imageElements;
        private readonly ITargetPrefix _targetPrefix;

        public TargetPrefixSerializer(
            ITargetPrefix targetPrefix,
            IEnumerable<IImageElement> imageElements) {
            this._targetPrefix = targetPrefix;
            this._imageElements = imageElements;
        }

        public uint Size => 274;

        public void Write(Stream stream) {
            using (var writer = new BinaryWriter(stream, Encoding.ASCII, true)) {
                writer.Write(this._targetPrefix.Signature.PadRight(6, '\x0000').ToCharArray(), 0, 6);
                writer.Write(Convert.ToByte(this._targetPrefix.AlternateSetting));
                writer.Write(this._targetPrefix.TargetNamed ? 1 : (uint) 0);
                writer.Write(this._targetPrefix.TargetName.PadRight(255, '\x0000').ToCharArray(), 0, 255);
                writer.Write(Convert.ToUInt32((int) this._imageElements.Sum(x => 4 + 4 + x.ElementSize)));
                writer.Write(Convert.ToUInt32(this._imageElements.Count()));
            }
        }
    }
}
