using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DfuToolCli.Tools.Elements.Remove {
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
            var targetIndex = string.IsNullOrEmpty(options.Index) ? -1 : options.Index.ToInt32(0, 255);

            var dfuSerializer = this._createDfuSerializer();
            var dfuDeserializer = this._createDfuDeserializer();

            using (var stream = new FileStream(options.File, FileMode.Open, FileAccess.ReadWrite)) {
                var dfu = dfuDeserializer.Read(stream);

                if (targetId >= 0) {
                    var image = dfu.Images.Images.FirstOrDefault(x => x.Prefix.TargetId == targetId);

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

        internal void ProcessInternal(Stream dfuStream, int targetId, int elementIndex) {
            var dfuSerializer = this._createDfuSerializer();
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

            image.ImageElements.RemoveAt(elementIndex);
            image.Prefix.NbElements--;
            image.Prefix.TargetSize -= 8 + element.ElementSize;

            dfuStream.Seek(0, SeekOrigin.Begin);
            dfuStream.SetLength(0);
            dfuSerializer.Write(dfuStream, dfu);
        }
    }
}
