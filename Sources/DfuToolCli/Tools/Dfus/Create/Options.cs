using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Dfus.Create {
    [Verb("dfu-create", HelpText = "Create empty DFU.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "set-device",
            Required = true,
            Default = "0xffff",
            HelpText = "Firmware version or 0xffff if ignored.")]
        public string SetDevice { get; set; }

        [Option(
            "set-product",
            Required = true,
            Default = "0xffff",
            HelpText = "Device's Product ID or 0xffff if ignored.")]
        public string SetProduct { get; set; }

        [Option(
            "set-vendor",
            Required = true,
            Default = "0xffff",
            HelpText = "Device's Vendor ID or 0xffff if ignored.")]
        public string SetVendor { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Create empty DFU file",
                    new Options {
                        File = "sample.dfu"
                    }),
                new Example(
                    "Create empty DFU file with specified IDs",
                    new Options {
                        File = "sample.dfu",
                        SetDevice = "0x5678",
                        SetProduct = "0x1234",
                        SetVendor = "0x0483"
                    })
            };
    }
}
