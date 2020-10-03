using DfuLib.Interfaces;

namespace DfuLib.Parts {
    internal class Dfu : IDfu {
        public IDfuPrefix Prefix { get; set; }
        public IDfuImages Images { get; set; }
        public IDfuSuffix Suffix { get; set; }
    }
}
