using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Targets.Clear {
    [Verb("target-clear", HelpText = "Remove all image elements from target.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "id",
            SetName = "ById",
            Required = false,
            Default = "",
            HelpText = "Alternate Setting (Target ID) to clear.")]
        public string Id { get; set; }

        [Option(
            "index",
            SetName = "ByIndex",
            Required = false,
            Default = "",
            HelpText = "Alternate Setting's (Target ID) index to clear.")]
        public string Index { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Remove all image elements from target by ID",
                    new Options {
                        File = "sample.dfu",
                        Id = "1"
                    }),
                new Example(
                    "Remove all image elements from target by index",
                    new Options {
                        File = "sample.dfu",
                        Index = "1"
                    })
            };
    }
}
