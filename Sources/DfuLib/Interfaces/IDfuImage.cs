using System.Collections.Generic;

namespace DfuLib.Interfaces {
    /// <summary>
    /// The DFU Image contains the effective data of the firmware, starting by a Target prefix record
    /// followed by a number of Image elements.
    /// </summary>
    public interface IDfuImage {
        ITargetPrefix Prefix { get; set; }
        List<IImageElement> ImageElements { get; }
    }
}
