using Autofac;
using DfuLib.Interfaces;

namespace DfuToolCli {
    public class DfuLogicModule : Module {
        protected override void Load(ContainerBuilder builder) {
            var dfuConvLib = typeof(IDfu).Assembly;
            builder.RegisterAssemblyTypes(dfuConvLib)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}
