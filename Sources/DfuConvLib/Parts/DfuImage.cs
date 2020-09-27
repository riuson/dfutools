using DfuConvLib.Interfaces;
using System.Collections.Generic;

namespace DfuConvLib.Parts {
    internal class DfuImage : IDfuImage {
        public ITargetPrefix Prefix { get; set; }

        public List<IImageElement> ImageElements { get; } = new List<IImageElement>();
    }
}
