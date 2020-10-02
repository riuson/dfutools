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
            Required = false,
            Default = "",
            HelpText = "ID of target to remove.")]
        public string TargetId { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Remove target by ID",
                    new Options {
                        File = "sample.dfu",
                        TargetId = "1"
                    })
            };
    }
}
