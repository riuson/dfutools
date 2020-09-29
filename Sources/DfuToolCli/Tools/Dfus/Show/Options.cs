using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Dfus.Show {
    [Verb("show", HelpText = "Show DFU contents.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            'l',
            "data-length",
            Default = 16,
            HelpText = "Length of element's data to display.")]
        public int ElementDataLength { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Show DFU content",
                    new Options {
                        File = "sample.dfu"
                    }),
                new Example(
                    "Show DFU content with specified amount of image element's data",
                    new Options {
                        File = "sample.dfu",
                        ElementDataLength = 8
                    })
            };
    }
}
