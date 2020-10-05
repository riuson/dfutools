using DfuLib.Interfaces;
using System;

namespace DfuToolGui.Controls.Editor {
    internal class DfuEditorViewModel {
        private readonly Func<IDfu> _createDfu;
        private readonly Func<IDfuImages> _createDfuImages;
        private readonly Func<IDfuPrefix> _createDfuPrefix;
        private readonly Func<IDfuSuffix> _createDfuSuffix;

        public DfuEditorViewModel(
            Func<IDfu> createDfu,
            Func<IDfuPrefix> createDfuPrefix,
            Func<IDfuImages> createDfuImages,
            Func<IDfuSuffix> createDfuSuffix) {
            this._createDfu = createDfu;
            this._createDfuPrefix = createDfuPrefix;
            this._createDfuImages = createDfuImages;
            this._createDfuSuffix = createDfuSuffix;

            this.Dfu = this._createDfu();
            this.Dfu.Prefix = this._createDfuPrefix();
            this.Dfu.Images = this._createDfuImages();
            this.Dfu.Suffix = this._createDfuSuffix();
            this.Dfu.Suffix.Vendor = 0x0483;
            this.Dfu.Suffix.Product = 0x1234;
            this.Dfu.Suffix.Device = 0x5678;
        }

        public IDfu Dfu { get; }
    }
}
