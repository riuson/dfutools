using Autofac;
using DfuConvLib.Interfaces;

namespace DfuConvCli {
    public class DfuLogicModule : Module {
        protected override void Load(ContainerBuilder builder) {
            var dfuConvLib = typeof(IDfu).Assembly;
            builder.RegisterAssemblyTypes(dfuConvLib)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}
