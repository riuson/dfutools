using Autofac;
using DfuToolCli.Tools;
using NUnit.Framework;

namespace DfuToolCli.Tests {
    /// <summary>
    /// Class to use IoC container in tests.
    /// </summary>
    public class BaseTestFixture {
        private readonly IContainer _container;

        public BaseTestFixture() {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new DfuLogicModule());
            builder.RegisterModule(new ToolsModule());
            this._container = builder.Build();
        }

        [OneTimeTearDown]
        public void TearDown() {
            this.ShutdownIoC();
        }

        protected TEntity Resolve<TEntity>() => this._container.Resolve<TEntity>();

        protected void ShutdownIoC() => this._container.Dispose();
    }
}
