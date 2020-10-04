using DfuLib.Interfaces;

namespace DfuToolGui.Controls.Demo {
    internal class DfuDemo : IDfu {
        public DfuDemo() {
            this.Prefix = new DfuPrefixDemo();
            this.Images = new DfuImagesDemo();
            this.Suffix = new DfuSuffixDemo();
        }

        public IDfuPrefix Prefix { get; set; }
        public IDfuImages Images { get; set; }
        public IDfuSuffix Suffix { get; set; }
    }
}
