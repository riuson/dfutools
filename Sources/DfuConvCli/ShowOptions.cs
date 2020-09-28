using CommandLine;
using CommandLine.Text;
using System.Collections.Generic;

namespace DfuConvCli {
    [Verb("show", HelpText = "Show DFU contents.")]
    internal class ShowOptions {
        [Value(0)] public string InputFile { get; set; }

        [Option(
            'l',
            "data-length",
            Default = 16,
            HelpText = "Length of element's data to display.")]
        public int ElementDataLength { get; set; }

        [Usage(ApplicationAlias = "dfuconvcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Show DFU content",
                    new ShowOptions {
                        InputFile = "sample.dfu"
                    }),
                new Example(
                    "Show DFU content with specified amount of image element's data",
                    new ShowOptions {
                        InputFile = "sample.dfu",
                        ElementDataLength = 8
                    })
            };
    }
}
