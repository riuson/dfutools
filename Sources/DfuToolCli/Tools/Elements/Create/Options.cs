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
            Default = "",
            HelpText = "ID of target to add image element.")]
        public string Id { get; set; }

        [Option(
            "content",
            Required = true,
            Default = "",
            HelpText = "Path to file with new image element's content")]
        public string ElementFile { get; set; }

        [Option(
            "address",
            Required = true,
            Default = "0xffffffff",
            HelpText = "Address of image element.")]
        public string ElementAddress { get; set; }

        [Usage(ApplicationAlias = "dfutoolcli")]
        public static IEnumerable<Example> Examples =>
            new List<Example> {
                new Example(
                    "Add new image element to target by ID",
                    new Options {
                        File = "sample.dfu",
                        Id = "1",
                        ElementFile = "element.bin",
                        ElementAddress = "0x08000000"
                    })
            };
    }
}
