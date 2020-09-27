using System.Collections.Generic;

namespace DfuConvLib.Interfaces {
    /// <summary>
    /// The images section placed between the prefix and suffix as a body of the DFU file, contains
    /// a list of DFU images indexed by the alternate setting.
    /// </summary>
    public interface IDfuImages {
        List<IDfuImage> Images { get; }
    }
}
