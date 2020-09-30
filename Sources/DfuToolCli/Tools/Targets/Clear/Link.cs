using DfuToolCli.Interfaces;
using System;

namespace DfuToolCli.Tools.Targets.Clear {
    internal class Link : ILink {
        private readonly Func<Processor> _createProcessor;

        public Link(Func<Processor> createProcessor) => this._createProcessor = createProcessor;

        public bool IsAcceptable(IVerbOptions options) => options is Options;

        public IVerbProcessor CreateProcessor() => this._createProcessor();
    }
}
