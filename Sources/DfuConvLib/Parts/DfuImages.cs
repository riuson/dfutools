using DfuConvLib.Interfaces;
using System.Collections.Generic;

namespace DfuConvLib.Parts {
    internal class DfuImages : IDfuImages {
        public List<IDfuImage> Images { get; } = new List<IDfuImage>();
    }
}
