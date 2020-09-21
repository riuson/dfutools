using System.Collections.Generic;

namespace DfuSeConvLib.Parts {
    /// <summary>
    /// The DFU Image contains the effective data of the firmware, starting by a Target prefix record
    /// followed by a number of Image elements.
    /// </summary>
    public class DfuImage {
        public TargetPrefix Prefix { get; set; }

        public virtual List<ImageElement> ImageElements { get; } = new List<ImageElement>();
    }
}
