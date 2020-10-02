using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Elements.Extract {
    [Verb("element-extract", HelpText = "Extract image element from specified target to file.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "id",
            Required = true,
            Default = "",
            HelpText = "Target ID to extract image element from it.")]
        public string TargetId { get; set; }

        [Option(
            "index",
            Required = true,
            Default = "",
            HelpText = "Index of image element to extract.")]
        public string ElementIndex { get; set; }

        [Option(
            "output-file",
            Required = true,
            Default = "",
            HelpText = "Index of image element to extract.")]
        public string OutputFile { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Extract image element from target to file.",
                    new Options {
                        File = "sample.dfu",
                        TargetId = "1",
                        ElementIndex = "0",
                        OutputFile = "element.bin"
                    })
            };
    }
}
