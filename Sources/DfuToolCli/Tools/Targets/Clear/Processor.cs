using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DfuToolCli.Tools.Targets.Clear {
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

            var targetId = string.IsNullOrEmpty(options.Id) ? -1 : options.Id.ToInt32(0, 255);
            var targetIndex = string.IsNullOrEmpty(options.Index) ? -1 : options.Index.ToInt32(0, 255);

            var dfuSerializer = this._createDfuSerializer();
            var dfuDeserializer = this._createDfuDeserializer();

            using (var stream = new FileStream(options.File, FileMode.Open, FileAccess.ReadWrite)) {
                var dfu = dfuDeserializer.Read(stream);

                if (targetId >= 0) {
                    var image = dfu.Images.Images.FirstOrDefault(x => x.Prefix.AlternateSetting == targetId);

                    if (image != null) {
                        image.ImageElements.Clear();
                    } else {
                        throw new ArgumentException($"Target with ID = {targetId} was not found!");
                    }
                } else if (targetIndex >= 0) {
                    if (targetIndex < dfu.Images.Images.Count) {
                        dfu.Images.Images[targetIndex].ImageElements.Clear();
                    } else {
                        throw new IndexOutOfRangeException(
                            $"Target with index == {targetIndex} not found in list of size {dfu.Images.Images.Count}!");
                    }
                }

                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);

                dfuSerializer.Write(stream, dfu);
            }
        }
    }
}
