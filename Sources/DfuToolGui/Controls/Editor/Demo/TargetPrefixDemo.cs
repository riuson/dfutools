using DfuLib.Interfaces;
using System;

namespace DfuToolGui.Controls.Editor.Demo {
    internal class TargetPrefixDemo : ITargetPrefix {
        private static readonly Random _rnd = new Random();

        public TargetPrefixDemo() {
            this.TargetId = _rnd.Next(0, 255);
            this.TargetName = "Hello, World!";
            this.TargetSize = Convert.ToUInt32(_rnd.Next(1, 16384));
        }

        public string Signature { get; set; }
        public int TargetId { get; set; }
        public bool IsTargetNamed { get; set; }
        public string TargetName { get; set; }
        public uint TargetSize { get; set; }
        public uint NbElements { get; set; }
    }
}
