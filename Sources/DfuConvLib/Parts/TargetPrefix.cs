using DfuConvLib.Interfaces;

namespace DfuConvLib.Parts {
    internal class TargetPrefix : ITargetPrefix {
        public string Signature { get; set; } = "Target";
        public int TargetId { get; set; } = 0;
        public bool IsTargetNamed { get; set; } = false;
        public string TargetName { get; set; } = string.Empty;
        public uint TargetSize { get; set; } = 0;
        public uint NbElements { get; set; } = 0;
    }
}
