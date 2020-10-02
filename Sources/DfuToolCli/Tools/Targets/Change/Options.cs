using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Targets.Change {
    [Verb("target-change", HelpText = "Change target's ID and Name.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "id",
            Required = true,
            Default = "",
            HelpText = "ID of target to change.")]
        public string TargetId { get; set; }

        [Option(
            "set-name",
            Required = false,
            Default = null,
            HelpText = "New target's Name (0...255 ASCII characters).")]
        public string SetTargetName { get; set; }

        [Option(
            "set-id",
            Required = false,
            Default = "",
            HelpText = "New target's ID.")]
        public string SetTargetId { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Change name of target by ID",
                    new Options {
                        File = "sample.dfu",
                        TargetId = "1",
                        SetTargetName = "newname"
                    }),
                new Example(
                    "Change ID of target by ID",
                    new Options {
                        File = "sample.dfu",
                        TargetId = "1",
                        SetTargetId = "2"
                    })
            };
    }
}
