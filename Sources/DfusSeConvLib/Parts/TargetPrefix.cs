using DfuSeConvLib.Interfaces;

namespace DfuSeConvLib.Parts {
    public class TargetPrefix : ITargetPrefix {
        public string Signature { get; set; } = "Target";
        public int AlternateSetting { get; set; } = 0;
        public bool TargetNamed { get; set; } = false;
        public string TargetName { get; set; } = string.Empty;
    }
}
