using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Elements.Remove {
    [Verb("element-remove", HelpText = "Remove image element from specified target.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "id",
            Required = true,
            Default = "",
            HelpText = "Target ID to remove image element.")]
        public string TargetId { get; set; }

        [Option(
            "index",
            Required = true,
            Default = "",
            HelpText = "Index of image element to remove.")]
        public string ElementIndex { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Remove image element from target.",
                    new Options {
                        File = "sample.dfu",
                        TargetId = "1",
                        ElementIndex = "0"
                    })
            };
    }
}
