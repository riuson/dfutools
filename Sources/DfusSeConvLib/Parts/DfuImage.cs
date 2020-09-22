using DfuSeConvLib.Interfaces;
using System.Collections.Generic;

namespace DfuSeConvLib.Parts {
    public class DfuImage : IDfuImage {
        public ITargetPrefix Prefix { get; set; }

        public List<IImageElement> ImageElements { get; } = new List<IImageElement>();
    }
}
