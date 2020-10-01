using DfuConvLib.Interfaces;
using DfuToolCli.Helpers;
using DfuToolCli.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DfuToolCli.Tools.Targets.Create {
    internal class Processor : IVerbProcessor {
        private readonly Func<IDfuDeserializer> _createDfuDeserializer;
        private readonly Func<IDfuImage> _createDfuImage;
        private readonly Func<IDfuSerializer> _createDfuSerializer;
        private readonly Func<ITargetPrefix> _createTargetPrefix;

        public Processor(
            Func<IDfuSerializer> createDfuSerializer,
            Func<IDfuDeserializer> createDfuDeserializer,
            Func<IDfuImage> createDfuImage,
            Func<ITargetPrefix> createTargetPrefix) {
            this._createDfuSerializer = createDfuSerializer;
            this._createDfuDeserializer = createDfuDeserializer;
            this._createDfuImage = createDfuImage;
            this._createTargetPrefix = createTargetPrefix;
        }

        public void Process(IVerbOptions obj) {
            var options = obj as Options;

            var setId = string.IsNullOrEmpty(options.SetId) ? -1 : options.SetId.ToInt32(0, 255);
            var setName = options.SetName;

            using (var stream = new FileStream(options.File, FileMode.Open, FileAccess.ReadWrite)) {
                this.ProcessInternal(stream, setId, setName);
            }
        }

        internal void ProcessInternal(Stream stream, int setId, string setName) {
            var dfuSerializer = this._createDfuSerializer();
            var dfuDeserializer = this._createDfuDeserializer();
            var dfu = dfuDeserializer.Read(stream);

            var targetPrefix = this._createTargetPrefix();

            if (setName == null) {
                targetPrefix.TargetName = string.Empty;
                targetPrefix.IsTargetNamed = false;
            } else {
                targetPrefix.TargetName = setName;
                targetPrefix.IsTargetNamed = true;
            }

            var usedIds = dfu.Images.Images
                .Select(x => x.Prefix.TargetId)
                .OrderBy(x => x)
                .ToArray();

            var allIds = Enumerable.Range(0, 256)
                .ToArray();

            var availableIds = allIds
                .Except(usedIds)
                .OrderBy(x => x)
                .ToArray();

            if (availableIds.Length == 0) {
                throw new ArgumentException("There is no available Target IDs in this DFU.");
            }

            if (setId == -1) {
                setId = availableIds.First();
            } else {
                if (!availableIds.Contains(setId)) {
                    throw new ArgumentException("Specified Target ID is exists already in this DFU.");
                }
            }

            targetPrefix.TargetId = setId;

            var dfuImage = this._createDfuImage();
            dfuImage.Prefix = targetPrefix;

            dfu.Images.Images.Add(dfuImage);


            stream.Seek(0, SeekOrigin.Begin);
            stream.SetLength(0);

            dfuSerializer.Write(stream, dfu);
        }
    }
}
