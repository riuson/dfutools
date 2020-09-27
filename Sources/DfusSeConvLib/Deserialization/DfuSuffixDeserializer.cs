using DfuSeConvLib.Exceptions;
using DfuSeConvLib.Helpers;
using DfuSeConvLib.Interfaces;
using System;
using System.IO;
using System.Text;

namespace DfuSeConvLib.Deserialization {
    internal class DfuSuffixDeserializer : IDfuSuffixDeserializer {
        private readonly Func<IDfuSuffix> _createDfuSuffix;
        private readonly DeserializerException.Factory _createException;

        public DfuSuffixDeserializer(
            DeserializerException.Factory createException,
            Func<IDfuSuffix> createDfuSuffix) {
            this._createException = createException;
            this._createDfuSuffix = createDfuSuffix;
        }

        public IDfuSuffix Read(Stream stream) {
            using (var reader = new BinaryReader(stream, Encoding.ASCII, true)) {
                var device = reader.ReadUInt16();
                var product = reader.ReadUInt16();
                var vendor = reader.ReadUInt16();
                var dfu = reader.ReadUInt16();

                if (dfu != 0x011a) {
                    throw this._createException(
                        $"DfuSuffix's field bcdDFU invalid: expected 0x011a, actual {dfu}",
                        stream.Position);
                }

                var chars = reader.ReadChars(3);
                var signature = new string(chars);
                signature = signature.TrimEnd('\x00');

                if (signature != "UFD") {
                    throw this._createException($"DfuSuffix's signature invalid: '{signature}'", stream.Position);
                }

                var length = reader.ReadByte();

                if (length != 16) {
                    throw this._createException(
                        $"DfuSuffix's field bLength invalid: expected 16, actual {length}",
                        stream.Position);
                }

                var position = stream.Position;
                stream.Seek(0, SeekOrigin.Begin);

                var buffer = new byte[4096];
                var crc32Calculated = Crc32.Init();

                // It is needed to read entire stream except last 4 bytes.
                while (stream.CanRead) {
                    var readed = stream.Read(buffer, 0, buffer.Length);

                    if (readed > 0) {
                        var remaining = stream.Length - stream.Position;

                        if (remaining > 4) {
                            crc32Calculated = Crc32.ComputePart(crc32Calculated, buffer, 0, Convert.ToUInt32(readed));
                        } else if (remaining == 4) {
                            break;
                        } else {
                            // remaining < 4
                            var missed = 4 - remaining;
                            readed -= Convert.ToInt32(missed);
                            //stream.Position -= missed;
                            crc32Calculated = Crc32.ComputePart(crc32Calculated, buffer, 0, Convert.ToUInt32(readed));

                            break;
                        }
                    } else {
                        break;
                    }
                }

                crc32Calculated = Crc32.Finish(crc32Calculated);
                crc32Calculated ^= 0xffffffffu;

                stream.Seek(position, SeekOrigin.Begin);

                var crc32Readed = reader.ReadUInt32();

                if (crc32Readed != crc32Calculated) {
                    throw this._createException(
                        $"DfuSuffix's checksum invalid: calculated 0x{crc32Calculated:X8}, stored 0x{crc32Readed:X8}",
                        stream.Position);
                }


                var dfuSuffix = this._createDfuSuffix();
                dfuSuffix.Device = device;
                dfuSuffix.Product = product;
                dfuSuffix.Vendor = vendor;
                dfuSuffix.Dfu = dfu;
                dfuSuffix.DfuSignature = signature;
                dfuSuffix.Length = length;
                dfuSuffix.Crc = crc32Readed;
                return dfuSuffix;
            }
        }
    }
}
