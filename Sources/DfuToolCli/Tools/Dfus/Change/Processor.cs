using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;

namespace DfuToolCli.Tools.Dfus.Change {
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

            var device = options.SetDevice == string.Empty ? -1 : options.SetDevice.ToInt32(0, 0xffff);
            var product = options.SetProduct == string.Empty ? -1 : options.SetProduct.ToInt32(0, 0xffff);
            var vendor = options.SetVendor == string.Empty ? -1 : options.SetVendor.ToInt32(0, 0xffff);

            var dfuSerializer = this._createDfuSerializer();

            using (var stream = new FileStream(options.File, FileMode.Create, FileAccess.ReadWrite)) {
                var dfu = this._createDfu();

                dfu.Prefix = this._createDfuPrefix();

                dfu.Images = this._createDfuImages();

                dfu.Suffix = this._createDfuSuffix();

                if (device >= 0) {
                    dfu.Suffix.Device = device;
                }

                if (product >= 0) {
                    dfu.Suffix.Product = product;
                }

                if (vendor >= 0) {
                    dfu.Suffix.Vendor = vendor;
                }

                dfuSerializer.Write(stream, dfu);
            }
        }
    }
}
