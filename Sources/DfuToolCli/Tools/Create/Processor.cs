using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;

namespace DfuToolCli.Tools.Create {
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

            var device = options.Device.ToInt32(0, 0xffff);
            var product = options.Product.ToInt32(0, 0xffff);
            var vendor = options.Vendor.ToInt32(0, 0xffff);

            var dfuSerializer = this._createDfuSerializer();

            using (var stream = new FileStream(options.File, FileMode.Create, FileAccess.ReadWrite)) {
                var dfu = this._createDfu();

                dfu.Prefix = this._createDfuPrefix();

                dfu.Images = this._createDfuImages();

                dfu.Suffix = this._createDfuSuffix();
                dfu.Suffix.Device = device;
                dfu.Suffix.Product = product;
                dfu.Suffix.Vendor = vendor;

                dfuSerializer.Write(stream, dfu);
            }
        }
    }
}
