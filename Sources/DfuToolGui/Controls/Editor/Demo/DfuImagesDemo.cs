using DfuLib.Interfaces;
using System.Collections.Generic;

namespace DfuToolGui.Controls.Editor.Demo {
    internal class DfuImagesDemo : IDfuImages {
        public DfuImagesDemo() =>
            this.Images = new List<IDfuImage> {
                new DfuImageDemo(),
                new DfuImageDemo(),
                new DfuImageDemo(),
                new DfuImageDemo()
            };

        public List<IDfuImage> Images { get; }
    }
}
