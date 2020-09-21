using System;

namespace DfuSeConvLib.Parts {
    /// <summary>
    /// The Image element structured as follows, provides a data record containing the effective
    /// firmware data preceded by the data address and data size.The Image Element buffer is
    /// represented in Big Endian order.
    /// </summary>
    public class ImageElement {
        /// <summary>
        /// The ElementAddress field gives the 4-byte starting address of the data.
        /// </summary>
        public uint ElementAddress { get; set; }

        /// <summary>
        /// The ElementSize field gives the size of the contained data.
        /// </summary>
        public uint ElementSize => Convert.ToUInt32(this.Data.Length);

        /// <summary>
        /// The Data field present the effective data.
        /// </summary>
        public byte[] Data { get; set; }
    }
}
