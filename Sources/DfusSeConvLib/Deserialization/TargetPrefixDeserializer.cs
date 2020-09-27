using DfuSeConvLib.Exceptions;
using DfuSeConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuSeConvLib.Deserialization {
    internal class TargetPrefixDeserializer : ITargetPrefixDeserializer {
        private readonly DeserializerException.Factory _createException;
        private readonly Func<ITargetPrefix> _createTargetPrefix;

        public TargetPrefixDeserializer(
            DeserializerException.Factory createException,
            Func<ITargetPrefix> createTargetPrefix) {
            this._createException = createException;
            this._createTargetPrefix = createTargetPrefix;
        }

        public ITargetPrefix Read(Stream stream) {
            using (var reader = new BinaryReader(stream, Encoding.ASCII, true)) {
                var chars = reader.ReadChars(6);
                var signature = new string(chars);
                signature = signature.TrimEnd('\x00');

                if (signature != "Target") {
                    throw this._createException($"TargetPrefix's signature invalid: '{signature}'", stream.Position);
                }

                var alternateSetting = reader.ReadByte();

                var targetNamed = reader.ReadUInt32();

                var targetName = string.Empty;

                if (targetNamed != 0) {
                    chars = reader.ReadChars(255);
                    targetName = new string(chars);
                    targetName = targetName.TrimEnd('\x00');
                }

                var targetSize = reader.ReadUInt32();
                var numberOfElements = reader.ReadUInt32();

                var targetPrefix = this._createTargetPrefix();
                targetPrefix.Signature = signature;
                targetPrefix.AlternateSetting = alternateSetting;
                targetPrefix.TargetNamed = targetNamed != 0;
                targetPrefix.TargetName = targetName;
                targetPrefix.TargetSize = targetSize;
                targetPrefix.NbElements = numberOfElements;
                return targetPrefix;
            }
        }
    }
}
