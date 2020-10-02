using CommandLine;
using CommandLine.Text;
using DfuToolCli.Interfaces;
using System.Collections.Generic;

namespace DfuToolCli.Tools.Elements.Create {
    [Verb("element-create", HelpText = "Add new image element to specified target.")]
    internal class Options : IVerbOptions {
        [Value(0)] public string File { get; set; }

        [Option(
            "id",
            Required = true,
            HelpText = "ID of target to add image element.")]
        public string TargetId { get; set; }

        [Option(
            "set-content",
            Required = true,
            HelpText = "Path to file with new image element's content")]
        public string SetElementFile { get; set; }

        [Option(
            "set-address",
            Required = true,
            Default = "0xffffffff",
            HelpText = "Address of image element.")]
        public string SetElementAddress { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Add new image element to target by ID",
                    new Options {
                        File = "sample.dfu",
                        TargetId = "1",
                        SetElementFile = "element.bin",
                        SetElementAddress = "0x08000000"
                    })
            };
    }
}
