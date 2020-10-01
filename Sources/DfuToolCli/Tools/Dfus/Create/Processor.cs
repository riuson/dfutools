using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;

namespace DfuToolCli.Tools.Dfus.Create {
    internal class Processor : IVerbProcessor {
        private readonly Func<IDfu> _createDfu;
        private readonly Func<IDfuImages> _createDfuImages;
        private readonly Func<IDfuPrefix> _createDfuPrefix;
        private readonly Func<IDfuSerializer> _createDfuSerializer;
        private readonly Func<IDfuSuffix> _createDfuSuffix;

        public Processor(
            Func<IDfuSerializer> createDfuSerializer,
            Func<IDfu> createDfu,
            Func<IDfuPrefix> createDfuPrefix,
            Func<IDfuImages> createDfuImages,
            Func<IDfuSuffix> createDfuSuffix) {
            this._createDfuSerializer = createDfuSerializer;
            this._createDfu = createDfu;
            this._createDfuPrefix = createDfuPrefix;
            this._createDfuImages = createDfuImages;
            this._createDfuSuffix = createDfuSuffix;
        }

        public void Process(IVerbOptions obj) {
            var options = obj as Options;

            var setDevice = options.SetDevice.ToInt32(0, 0xffff);
            var setProduct = options.SetProduct.ToInt32(0, 0xffff);
            var setVendor = options.SetVendor.ToInt32(0, 0xffff);

            using (var stream = new FileStream(options.File, FileMode.Create, FileAccess.ReadWrite)) {
                this.ProcessInternal(stream, setDevice, setProduct, setVendor);
            }
        }

        internal void ProcessInternal(Stream stream, int setDevice, int setProduct, int setVendor) {
            var dfuSerializer = this._createDfuSerializer();
            var dfu = this._createDfu();

            dfu.Prefix = this._createDfuPrefix();
            dfu.Images = this._createDfuImages();
            dfu.Suffix = this._createDfuSuffix();
            dfu.Suffix.Device = setDevice;
            dfu.Suffix.Product = setProduct;
            dfu.Suffix.Vendor = setVendor;

            dfuSerializer.Write(stream, dfu);
        }
    }
}
