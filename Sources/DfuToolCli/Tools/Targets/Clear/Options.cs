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
            Required = true,
            HelpText = "ID of target to clear image elements.")]
        public string TargetId { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Remove all image elements from target by ID",
                    new Options {
                        File = "sample.dfu",
                        TargetId = "1"
                    })
            };
    }
}
