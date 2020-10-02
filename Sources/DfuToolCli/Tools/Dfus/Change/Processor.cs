using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;

namespace DfuToolCli.Tools.Dfus.Change {
    internal class Processor : IVerbProcessor {
        private readonly Func<IDfu> _createDfu;
        private readonly Func<IDfuDeserializer> _createDfuDeserializer;
        private readonly Func<IDfuImages> _createDfuImages;
        private readonly Func<IDfuPrefix> _createDfuPrefix;
        private readonly Func<IDfuSerializer> _createDfuSerializer;
        private readonly Func<IDfuSuffix> _createDfuSuffix;

        public Processor(
            Func<IDfuSerializer> createDfuSerializer,
            Func<IDfuDeserializer> createDfuDeserializer,
            Func<IDfu> createDfu,
            Func<IDfuPrefix> createDfuPrefix,
            Func<IDfuImages> createDfuImages,
            Func<IDfuSuffix> createDfuSuffix) {
            this._createDfuSerializer = createDfuSerializer;
            this._createDfuDeserializer = createDfuDeserializer;
            this._createDfu = createDfu;
            this._createDfuPrefix = createDfuPrefix;
            this._createDfuImages = createDfuImages;
            this._createDfuSuffix = createDfuSuffix;
        }

        public void Process(IVerbOptions obj) {
            var options = obj as Options;

            var setDevice = options.SetDevice == string.Empty ? -1 : options.SetDevice.ToInt32(0, 0xffff);
            var setProduct = options.SetProduct == string.Empty ? -1 : options.SetProduct.ToInt32(0, 0xffff);
            var setVendor = options.SetVendor == string.Empty ? -1 : options.SetVendor.ToInt32(0, 0xffff);

            using (var stream = new FileStream(options.File, FileMode.Open, FileAccess.ReadWrite)) {
                this.ProcessInternal(stream, setDevice, setProduct, setVendor);
            }
        }

        internal void ProcessInternal(Stream stream, int setDevice, int setProduct, int setVendor) {
            var dfuDeserializer = this._createDfuDeserializer();
            var dfu = dfuDeserializer.Read(stream);

            if (setDevice >= 0) {
                dfu.Suffix.Device = setDevice;
            }

            if (setProduct >= 0) {
                dfu.Suffix.Product = setProduct;
            }

            if (setVendor >= 0) {
                dfu.Suffix.Vendor = setVendor;
            }

            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);

            var dfuSerializer = this._createDfuSerializer();
            dfuSerializer.Write(stream, dfu);
        }
    }
}
