using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DfuToolCli.Tools.Elements.Create {
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
            var setElementAddress =
                string.IsNullOrEmpty(options.SetElementAddress) ? 0xffffffffu : options.SetElementAddress.ToUInt32();
            var setElementFile = options.SetElementFile;

            if (!File.Exists(setElementFile)) {
                throw new ArgumentException("Element's file was not found!");
            }

            using (var dfuStream = new FileStream(options.File, FileMode.Open, FileAccess.ReadWrite)) {
                using (var elementStream = new FileStream(setElementFile, FileMode.Open, FileAccess.Read)) {
                    this.ProcessInternal(dfuStream, targetId, setElementAddress, elementStream);
                }
            }
        }

        public void ProcessInternal(Stream dfuStream, int id, uint setElementAddress, Stream setElementStream) {
            var dfuSerializer = this._createDfuSerializer();
            var dfuDeserializer = this._createDfuDeserializer();

            var dfu = dfuDeserializer.Read(dfuStream);

            IDfuImage image = null;

            if (id >= 0) {
                image = dfu.Images.Images.FirstOrDefault(x => x.Prefix.TargetId == id);
            }

            if (image == null) {
                throw new ArgumentException("Target was not found!");
            }

            var imageElement = this._createImageElement();
            imageElement.ElementAddress = setElementAddress;
            var buffer = new byte[setElementStream.Length];
            setElementStream.Read(buffer, 0, buffer.Length);
            imageElement.Data = buffer;
            imageElement.ElementSize = Convert.ToUInt32(buffer.Length);

            image.ImageElements.Add(imageElement);
            image.Prefix.NbElements++;
            image.Prefix.TargetSize += 8 + imageElement.ElementSize;

            dfuStream.Seek(0, SeekOrigin.Begin);
            dfuStream.SetLength(0);
            dfuSerializer.Write(dfuStream, dfu);
        }
    }
}
