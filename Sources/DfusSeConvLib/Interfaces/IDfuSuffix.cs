namespace DfuSeConvLib.Interfaces {
    /// <summary>
    /// The DFU Suffix, as specified in the DFU specification, allows the host software to detect and
    /// prevent attempts to download incompatible firmware.The Suffix buffer is represented in
    /// Little Endian order.
    /// </summary>
    public interface IDfuSuffix {
        /// <summary>
        /// The Device field gives the firmware version contained in the file, or 0xFFFF if ignored.
        /// </summary>
        int Device { get; set; }

        /// <summary>
        /// The Product field give the Product ID of the
        /// device that the file is intended for, or 0xFFFF if the field is ignored.
        /// </summary>
        int Product { get; set; }

        /// <summary>
        /// The Vendor field give the Vendor ID of the
        /// device that the file is intended for, or 0xFFFF if the field is ignored.
        /// </summary>
        int Vendor { get; set; }

        /// <summary>
        /// The DFU field, fixed to 0x011A, gives the DFU specification number. This value
        /// differs from that specified in standard DFU rev1.1.
        /// </summary>
        int Dfu { get; set; }

        /// <summary>
        /// The Signature field contains a fixed string of three unsigned characters (44h, 46h,
        /// 55h). In the file they appear in reverse order, allowing valid DFU files to be recognized.
        /// </summary>
        string DfuSignature { get; set; }

        /// <summary>
        /// The Length field, currently fixed to 16, gives the length of the DFU Suffix itself in bytes.
        /// </summary>
        int Length { get; set; }

        /// <summary>
        /// The CRC (Cyclic Redundancy Check) field is the CRC calculated over the whole file
        /// except for the CRC data itself.
        /// </summary>
        uint Crc { get; set; }
    }
}
