using DfuLib.Interfaces;

namespace DfuToolGui.Controls.Demo {
    internal class DfuPrefixDemo : IDfuPrefix {
        public string Signature { get; set; }
        public int Version { get; set; }
        public uint DfuImageSize { get; set; }
        public int Targets { get; set; }
    }
}
