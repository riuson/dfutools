using DfuLib.Interfaces;
using System.Collections.Generic;

namespace DfuLib.Parts {
    internal class DfuImage : IDfuImage {
        public ITargetPrefix Prefix { get; set; }

        public List<IImageElement> ImageElements { get; } = new List<IImageElement>();
    }
}
