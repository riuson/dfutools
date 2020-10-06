using DfuLib.Interfaces;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.IO;

namespace DfuToolGui.Controls.Editor {
    public class DfuEditorViewModel : ReactiveObject {
        private readonly Func<IDfuDeserializer> _createDeserializer;
        private readonly Func<IDfu> _createDfu;
        private readonly Func<IDfuImages> _createDfuImages;
        private readonly Func<IDfuPrefix> _createDfuPrefix;
        private readonly Func<IDfuSuffix> _createDfuSuffix;
        private readonly Func<IDfuSerializer> _createSerializer;

        public DfuEditorViewModel(
            Func<IDfu> createDfu,
            Func<IDfuPrefix> createDfuPrefix,
            Func<IDfuImages> createDfuImages,
            Func<IDfuSuffix> createDfuSuffix,
            Func<IDfuSerializer> createSerializer,
            Func<IDfuDeserializer> createDeserializer) {
            this._createDfu = createDfu;
            this._createDfuPrefix = createDfuPrefix;
            this._createDfuImages = createDfuImages;
            this._createDfuSuffix = createDfuSuffix;
            this._createSerializer = createSerializer;
            this._createDeserializer = createDeserializer;

            this.Dfu = this._createDfu();
            this.Dfu.Prefix = this._createDfuPrefix();
            this.Dfu.Images = this._createDfuImages();
            this.Dfu.Suffix = this._createDfuSuffix();
            this.Dfu.Suffix.Vendor = 0x0483;
            this.Dfu.Suffix.Product = 0;
            this.Dfu.Suffix.Device = 0;
        }

        [Reactive] public IDfu Dfu { get; set; }

        public void Save(string fileName) {
            using (var stream = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read)) {
                var serializer = this._createSerializer();
                serializer.Write(stream, this.Dfu);
            }
        }

        public void Load(string fileName) {
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read)) {
                var deserializer = this._createDeserializer();
                this.Dfu = deserializer.Read(stream);
            }
        }
    }
}
