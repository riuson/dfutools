using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Dfus.Change {
    [Verb("dfu-change", HelpText = "Change IDs in DFU file.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "set-device",
            Required = false,
            HelpText = "New firmware version.")]
        public string SetDevice { get; set; }

        [Option(
            "set-product",
            Required = false,
            HelpText = "New Product ID of device.")]
        public string SetProduct { get; set; }

        [Option(
            "set-vendor",
            Required = false,
            HelpText = "New Vendor ID of device.")]
        public string SetVendor { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Change Product ID of DFU file",
                    new Options {
                        File = "sample.dfu",
                        SetProduct = "0x1234"
                    })
            };
    }
}
