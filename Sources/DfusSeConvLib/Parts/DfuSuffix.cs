using DfuSeConvLib.Interfaces;

namespace DfuSeConvLib.Parts {
    public class DfuSuffix : IDfuSuffix {
        public int Device { get; set; } = 0xffff;
        public int Product { get; set; } = 0xffff;
        public int Vendor { get; set; } = 0xffff;
        public int Dfu { get; set; } = 0x011a;
        public string DfuSignature { get; set; } = "UFD";
        public int Length { get; set; } = 0x10;
        public uint Crc { get; set; } = 0;
    }
}
