using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Targets.Change {
    [Verb("target-change", HelpText = "Change target's ID and name.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "id",
            SetName = "ById",
            Required = true,
            Default = "",
            HelpText = "Alternate Setting (Target ID) to clear.")]
        public string Id { get; set; }

        [Option(
            "index",
            SetName = "ByIndex",
            Required = true,
            Default = "",
            HelpText = "Alternate Setting's (Target ID) index to clear.")]
        public string Index { get; set; }

        [Option(
            "set-name",
            Required = false,
            Default = null,
            HelpText = "Target name (0...255 ASCII characters).")]
        public string SetName { get; set; }

        [Option(
            "set-id",
            Required = false,
            Default = "",
            HelpText = "Alternate Setting (Target ID).")]
        public string SetId { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Change name of target by ID",
                    new Options {
                        File = "sample.dfu",
                        Id = "1",
                        SetName = "newname"
                    }),
                new Example(
                    "Change ID of target by index",
                    new Options {
                        File = "sample.dfu",
                        Index = "1",
                        SetId = "2"
                    }),
                new Example(
                    "Change ID of target by ID",
                    new Options {
                        File = "sample.dfu",
                        Id = "1",
                        SetId = "2"
                    })
            };
    }
}
