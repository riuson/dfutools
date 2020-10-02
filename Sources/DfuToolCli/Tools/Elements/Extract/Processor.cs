using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DfuToolCli.Tools.Elements.Extract {
    internal class Processor : IVerbProcessor {
        private readonly Func<IDfuDeserializer> _createDfuDeserializer;
        private readonly Func<IDfuSerializer> _createDfuSerializer;
        private readonly Func<IImageElement> _createImageElement;

        public Processor(
            Func<IDfuSerializer> createDfuSerializer,
            Func<IDfuDeserializer> createDfuDeserializer,
            Func<IImageElement> createImageElement) {
            this._createDfuSerializer = createDfuSerializer;
            this._createDfuDeserializer = createDfuDeserializer;
            this._createImageElement = createImageElement;
        }

        public void Process(IVerbOptions obj) {
            var options = obj as Options;

            var targetId = string.IsNullOrEmpty(options.TargetId) ? -1 : options.TargetId.ToInt32(0, 255);
            var elementIndex = string.IsNullOrEmpty(options.ElementIndex) ? -1 : options.ElementIndex.ToInt32(0, 255);
            var outputFile = options.OutputFile;

            using (var dfuStream = new FileStream(options.File, FileMode.Open, FileAccess.ReadWrite)) {
                using (var outputStream = new FileStream(outputFile, FileMode.Create, FileAccess.ReadWrite)) {
                    this.ProcessInternal(dfuStream, targetId, elementIndex, outputStream);
                }
            }
        }

        internal void ProcessInternal(Stream dfuStream, int targetId, int elementIndex, Stream outputStream) {
            var dfuDeserializer = this._createDfuDeserializer();

            var dfu = dfuDeserializer.Read(dfuStream);

            IDfuImage image = null;

            if (targetId >= 0) {
                image = dfu.Images.Images.FirstOrDefault(x => x.Prefix.TargetId == targetId);
            }

            if (image == null) {
                throw new ArgumentException("Target was not found!");
            }

            if (elementIndex >= image.ImageElements.Count) {
                throw new IndexOutOfRangeException("Element's index out of range!");
            }

            var element = image.ImageElements[elementIndex];

            outputStream.Write(element.Data, 0, element.Data.Length);

            dfuStream.Seek(0, SeekOrigin.Begin);
            dfuStream.SetLength(0);

            var dfuSerializer = this._createDfuSerializer();
            dfuSerializer.Write(dfuStream, dfu);
        }
    }
}
