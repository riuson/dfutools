using DfuConvLib.Interfaces;

namespace DfuConvLib.Parts {
    internal class Dfu : IDfu {
        public IDfuPrefix Prefix { get; set; }
        public IDfuImages Images { get; set; }
        public IDfuSuffix Suffix { get; set; }
    }
}
