using System.Collections.Generic;

namespace DfuSeConvLib.Parts {
    /// <summary>
    /// The images section placed between the prefix and suffix as a body of the DFU file, contains
    /// a list of DFU images indexed by the alternate setting.
    /// </summary>
    public class DfuImages {
        public List<DfuImage> Images { get; } = new List<DfuImage>();
    }
}
