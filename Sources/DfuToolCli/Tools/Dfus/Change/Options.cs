using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Dfus.Change {
    [Verb("change", HelpText = "Change IDs of DFU file.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            'd',
            "set-device",
            Required = false,
            Default = "",
            HelpText = "New firmware version.")]
        public string SetDevice { get; set; }

        [Option(
            'p',
            "set-product",
            Required = false,
            Default = "",
            HelpText = "New device's Product ID.")]
        public string SetProduct { get; set; }

        [Option(
            'v',
            "set-vendor",
            Required = false,
            Default = "",
            HelpText = "New device's VEndor ID.")]
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
