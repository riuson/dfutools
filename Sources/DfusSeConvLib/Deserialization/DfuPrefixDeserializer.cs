using DfuSeConvLib.Exceptions;
using DfuSeConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuSeConvLib.Deserialization {
    internal class DfuPrefixDeserializer : IDfuPrefixDeserializer {
        private readonly Func<IDfuPrefix> _createDfuPrefix;
        private readonly DeserializerException.Factory _createException;

        public DfuPrefixDeserializer(
            DeserializerException.Factory createException,
            Func<IDfuPrefix> createDfuPrefix) {
            this._createException = createException;
            this._createDfuPrefix = createDfuPrefix;
        }

        public IDfuPrefix Read(Stream stream) {
            using (var reader = new BinaryReader(stream, Encoding.ASCII, true)) {
                var chars = reader.ReadChars(5);
                var signature = new string(chars);
                signature = signature.TrimEnd('\x00');

                if (signature != "DfuSe") {
                    throw this._createException($"DfuPrefix's signature invalid: '{signature}'", stream.Position);
                }

                var version = reader.ReadByte();

                if (version != 1) {
                    throw this._createException($"DfuPrefix's version not supported: {version}", stream.Position);
                }

                var dfuImageSize = reader.ReadUInt32();

                if (dfuImageSize != stream.Length - 16) {
                    throw this._createException(
                        $"DfuPrefix's image length invalid: expected {stream.Length - 16}, actual {dfuImageSize}",
                        stream.Position);
                }

                var targets = reader.ReadByte();

                var dfuPrefix = this._createDfuPrefix();
                dfuPrefix.Signature = signature;
                dfuPrefix.Version = version;
                dfuPrefix.DfuImageSize = dfuImageSize;
                dfuPrefix.Targets = targets;
                return dfuPrefix;
            }
        }
    }
}
