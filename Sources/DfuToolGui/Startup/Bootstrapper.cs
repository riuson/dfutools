using Autofac;
using System.Text;

namespace DfuToolGui.Startup {
    public class Bootstrapper {
        private readonly ContainerBuilder _builder;
        private IContainer _container;

        public Bootstrapper() => this._builder = new ContainerBuilder();

        public void Shurdown() {
            this._container.Dispose();
        }

        public void Prepare(App app) {
            this._builder.RegisterInstance(app)
                .AsSelf()
                .SingleInstance();
            this._builder.RegisterAssemblyModules(typeof(Bootstrapper).Assembly);

#if NET_CORE
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var enc = Encoding.GetEncoding("ASCII");
#endif
        }

        public void Build() {
            this._container = this._builder.Build();
        }

        public T Resolve<T>() => this._container.Resolve<T>();
    }
}
