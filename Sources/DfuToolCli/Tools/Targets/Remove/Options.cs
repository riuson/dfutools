using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Targets.Remove {
    [Verb("target-remove", HelpText = "Remove existing target.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "id",
            SetName = "ById",
            Required = false,
            Default = "",
            HelpText = "Alternate Setting (Target ID) to remove.")]
        public string Id { get; set; }

        [Option(
            "index",
            SetName = "ByIndex",
            Required = false,
            Default = "",
            HelpText = "Alternate Setting's (Target ID) index to remove.")]
        public string Index { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Remove target by ID",
                    new Options {
                        File = "sample.dfu",
                        Id = "1"
                    }),
                new Example(
                    "Remove target by index",
                    new Options {
                        File = "sample.dfu",
                        Index = "1"
                    })
            };
    }
}
