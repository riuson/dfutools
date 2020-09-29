using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Dfus.Clear {
    [Verb("clear", HelpText = "Remove all targets from DFU file.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Remove all targets from DFU file",
                    new Options {
                        File = "sample.dfu"
                    })
            };
    }
}
