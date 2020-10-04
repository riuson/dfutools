using DfuLib.Interfaces;
using System.Collections.Generic;

namespace DfuToolGui.Controls.Demo {
    internal class DfuImageDemo : IDfuImage {
        public DfuImageDemo() {
            this.Prefix = new TargetPrefixDemo();
            this.ImageElements = new List<IImageElement> {
                new ImageElementDemo(),
                new ImageElementDemo()
            };
        }

        public ITargetPrefix Prefix { get; set; }
        public List<IImageElement> ImageElements { get; }
    }
}
