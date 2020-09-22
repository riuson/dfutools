using DfuSeConvLib.Interfaces;
using System.Collections.Generic;

namespace DfuSeConvLib.Parts {
    public class DfuImages : IDfuImages {
        public List<IDfuImage> Images { get; } = new List<IDfuImage>();
    }
}
