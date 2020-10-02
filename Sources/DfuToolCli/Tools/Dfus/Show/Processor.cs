using DfuConvLib.Interfaces;
using DfuToolCli.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace DfuToolCli.Tools.Dfus.Show {
    internal class Processor : IVerbProcessor {
        private readonly Func<IDfuDeserializer> _createDfuDeserializer;

        public Processor(Func<IDfuDeserializer> createDfuDeserializer) =>
            this._createDfuDeserializer = createDfuDeserializer;

        public void Process(IVerbOptions obj) {
            var options = obj as Options;

            if (!File.Exists(options.File)) {
                Console.WriteLine("File not found!");
                return;
            }

            if (options.ElementDataLength < 0) {
                Console.WriteLine("Invalid argument!");
                return;
            }


            var dfuDeserializer = this._createDfuDeserializer();

            using (var stream = new FileStream(options.File, FileMode.Open, FileAccess.Read)) {
                var dfu = dfuDeserializer.Read(stream);

                var msg = string.Format(
                    "Prefix:{0}" +
                    "  ImageSize: {1}{0}" +
                    "  Targets: {2}{0}",
                    Environment.NewLine,
                    dfu.Prefix.DfuImageSize,
                    dfu.Prefix.Targets
                );
                Console.WriteLine(msg);

                msg = string.Format(
                    "Suffix:{0}" +
                    "  Device Version: 0x{1:x4}{0}" +
                    "  Product ID: 0x{2:x4}{0}" +
                    "  Vendor ID: 0x{3:x4}{0}" +
                    "  CRC32: 0x{4:x8}{0}",
                    Environment.NewLine,
                    dfu.Suffix.Device,
                    dfu.Suffix.Product,
                    dfu.Suffix.Vendor,
                    dfu.Suffix.Crc
                );
                Console.WriteLine(msg);

                for (var imageIndex = 0; imageIndex < dfu.Images.Images.Count; imageIndex++) {
                    var image = dfu.Images.Images[imageIndex];
                    msg = string.Format(
                        "Target [{1}]:{0}" +
                        "  Target ID: {2}{0}" +
                        "  Target Name: {3}{0}" +
                        "  Target Size: {4}{0}" +
                        "  Element's count: {5}{0}",
                        Environment.NewLine,
                        imageIndex,
                        image.Prefix.TargetId,
                        image.Prefix.IsTargetNamed ? image.Prefix.TargetName : "[none]",
                        image.Prefix.TargetSize,
                        image.Prefix.NbElements
                    );
                    Console.WriteLine(msg);

                    for (var elementIndex = 0; elementIndex < image.ImageElements.Count; elementIndex++) {
                        var element = image.ImageElements[elementIndex];
                        msg = string.Format(
                            "  Element [{1}]:{0}" +
                            "    Address: 0x{2:x8}{0}" +
                            "    Size: {3}",
                            Environment.NewLine,
                            elementIndex,
                            element.ElementAddress,
                            element.ElementSize
                        );
                        Console.WriteLine(msg);

                        if (options.ElementDataLength > 0) {
                            var content = string.Join(
                                " ",
                                element.Data
                                    .Take(options.ElementDataLength)
                                    .Select(x => x.ToString("x2")));
                            msg = string.Format(
                                "    Data (hex): {1}{2}{0}",
                                Environment.NewLine,
                                content,
                                options.ElementDataLength < element.Data.Length ? " ..." : string.Empty
                            );
                            Console.WriteLine(msg);
                        }
                    }
                }
            }
        }
    }
}
