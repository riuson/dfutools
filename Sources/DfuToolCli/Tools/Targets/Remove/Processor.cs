using DfuLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DfuToolCli.Tools.Targets.Remove {
    internal class Processor : IVerbProcessor {
        private readonly Func<IDfuDeserializer> _createDfuDeserializer;
        private readonly Func<IDfuSerializer> _createDfuSerializer;

        public Processor(
            Func<IDfuSerializer> createDfuSerializer,
            Func<IDfuDeserializer> createDfuDeserializer) {
            this._createDfuSerializer = createDfuSerializer;
            this._createDfuDeserializer = createDfuDeserializer;
        }

        public void Process(IVerbOptions obj) {
            var options = obj as Options;

            var targetId = string.IsNullOrEmpty(options.TargetId) ? -1 : options.TargetId.ToInt32(0, 255);

            using (var stream = new FileStream(options.File, FileMode.Open, FileAccess.ReadWrite)) {
                this.ProcessInternal(stream, targetId);
            }
        }

        internal void ProcessInternal(Stream stream, int targetId) {
            var dfuDeserializer = this._createDfuDeserializer();
            var dfu = dfuDeserializer.Read(stream);

            if (targetId >= 0) {
                var image = dfu.Images.Images.FirstOrDefault(x => x.Prefix.TargetId == targetId);

                if (image != null) {
                    dfu.Images.Images.Remove(image);
                } else {
                    throw new ArgumentException($"Target with ID = {targetId} was not found!");
                }
            }

            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);

            var dfuSerializer = this._createDfuSerializer();
            dfuSerializer.Write(stream, dfu);
        }
    }
}
