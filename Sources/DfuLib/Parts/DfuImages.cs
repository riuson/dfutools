using DfuLib.Interfaces;
using System.Collections.Generic;

namespace DfuLib.Parts {
    internal class DfuImages : IDfuImages {
        public List<IDfuImage> Images { get; } = new List<IDfuImage>();
    }
}
