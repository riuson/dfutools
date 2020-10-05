using DfuLib.Interfaces;
using System;
using System.Linq;

namespace DfuToolGui.Controls.Editor.Demo {
    internal class ImageElementDemo : IImageElement {
        private static readonly Random _rnd = new Random();

        public ImageElementDemo() {
            this.ElementAddress = 0x08000000u + Convert.ToUInt32(_rnd.Next(0, 1024 * 1024));
            this.ElementSize = Convert.ToUInt32(_rnd.Next(1, 16384));
            this.Data = Enumerable.Range(0, 1024)
                .Select(x => (byte) x)
                .ToArray();
        }

        public uint ElementAddress { get; set; }
        public uint ElementSize { get; set; }
        public byte[] Data { get; set; }
    }
}
