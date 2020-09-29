using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Change {
    [Verb("change", HelpText = "Change IDs of DFU file.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            'd',
            "device",
            Required = false,
            Default = "",
            HelpText = "New firmware version.")]
        public string Device { get; set; }

        [Option(
            'p',
            "product",
            Required = false,
            Default = "",
            HelpText = "New device's Product ID.")]
        public string Product { get; set; }

        [Option(
            'v',
            "vendor",
            Required = false,
            Default = "",
            HelpText = "New device's VEndor ID.")]
        public string Vendor { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Change Product ID of DFU file",
                    new Options {
                        File = "sample.dfu",
                        Product = "0x1234"
                    })
            };
    }
}
