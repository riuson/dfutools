using DfuConvLib.Interfaces;
using DfuToolCli.Interfaces;
using System;
using System.IO;

namespace DfuToolCli.Tools.Dfus.Clear {
    internal class Processor : IVerbProcessor {
        private readonly Func<IDfuDeserializer> _createDfuDeserializer;
        private readonly Func<IDfuSerializer> _createDfuSerializer;

        public Processor(
            Func<IDfuDeserializer> createDfuDeserializer,
            Func<IDfuSerializer> createDfuSerializer) {
            this._createDfuDeserializer = createDfuDeserializer;
            this._createDfuSerializer = createDfuSerializer;
        }

        public void Process(IVerbOptions obj) {
            var options = obj as Options;

            var dfuDeserializer = this._createDfuDeserializer();
            var dfuSerializer = this._createDfuSerializer();

            using (var stream = new FileStream(options.File, FileMode.Open, FileAccess.ReadWrite)) {
                var dfu = dfuDeserializer.Read(stream);
                dfu.Images.Images.Clear();

                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);

                dfuSerializer.Write(stream, dfu);
            }
        }
    }
}
