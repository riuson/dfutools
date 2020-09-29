using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Targets.Create {
    [Verb("target-create", HelpText = "Create new target.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "name",
            Required = false,
            Default = "",
            HelpText = "Target name (0...255 ASCII characters).")]
        public string Name { get; set; }

        [Option(
            "id",
            Required = false,
            Default = "",
            HelpText = "Alternate Setting (Target ID).")]
        public string Id { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Create new target with empty Name and first available ID",
                    new Options {
                        File = "sample.dfu"
                    }),
                new Example(
                    "Create new target with specified Name and ID",
                    new Options {
                        File = "sample.dfu",
                        Name = "Application",
                        Id = "1"
                    })
            };
    }
}
