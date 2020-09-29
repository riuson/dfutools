using Autofac;
using CommandLine;
using DfuToolCli.Interfaces;
using DfuToolCli.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DfuToolCli {
    internal class CommandsProcessor : IDisposable {
        private readonly IContainer _container;

        public CommandsProcessor() {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DfuLogicModule());
            builder.RegisterModule(new ToolsModule());
            this._container = builder.Build();
        }

        public void Dispose() {
            this._container?.Dispose();
        }

        protected TEntity Resolve<TEntity>() => this._container.Resolve<TEntity>();

        public void Process(string[] args) {
            var types = this.Resolve<IEnumerable<IVerbOptions>>()
                .Select(x => x.GetType())
                .ToArray();
            Parser.Default.ParseArguments(args, types)
                .WithParsed(this.RunForType)
                .WithNotParsed(this.HandleErrors);
        }

        private void HandleErrors(IEnumerable<Error> errors) {
            foreach (var error in errors) {
                Console.WriteLine(error.Tag);
            }
        }

        private void RunForType(object obj) {
            if (obj is IVerbOptions verbOptions) {
                var links = this.Resolve<IEnumerable<ILink>>();
                var link = links.FirstOrDefault(x => x.IsAcceptable(verbOptions));

                if (link != null) {
                    var verbProcessor = link.CreateProcessor();
                    verbProcessor.Process(verbOptions);
                }
            }
        }
    }
}
