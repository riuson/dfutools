using DfuLib.Interfaces;

namespace DfuToolGui.Controls.Demo {
    internal class DfuSuffixDemo : IDfuSuffix {
        public DfuSuffixDemo() {
            this.Vendor = 0x0483;
            this.Product = 0x1234;
            this.Device = 0x5678;
        }

        public int Device { get; set; }
        public int Product { get; set; }
        public int Vendor { get; set; }
        public int Dfu { get; set; }
        public string DfuSignature { get; set; }
        public int Length { get; set; }
        public uint Crc { get; set; }
    }
}
