using DfuConvLib.Exceptions;
using DfuConvLib.Helpers;
using DfuConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuConvLib.Deserialization {
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
                var signature = reader.ReadString(6);

                if (signature != "Target") {
                    throw this._createException($"TargetPrefix's signature invalid: '{signature}'",
                        stream.Position - 6);
                }

                var alternateSetting = reader.ReadByte();

                var targetNamed = reader.ReadUInt32();

                var targetName = string.Empty;

                if (targetNamed != 0) {
                    targetName = reader.ReadString(255);
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
