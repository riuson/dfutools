using Autofac;
using Autofac.Core;

namespace DfuLib.Tests {
    /// <summary>
    /// Class to use IoC container in tests.<br />
    /// https://juristr.com/blog/2011/12/writing-ioc-supported-integration-tests/
    /// </summary>
    /// <typeparam name="TModule"></typeparam>
    public class IoCSupportedTest<TModule> where TModule : IModule, new() {
        private readonly IContainer _container;

        public IoCSupportedTest() {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new TModule());
            this._container = builder.Build();
        }

        protected TEntity Resolve<TEntity>() => this._container.Resolve<TEntity>();

        protected void ShutdownIoC() => this._container.Dispose();
    }
}
