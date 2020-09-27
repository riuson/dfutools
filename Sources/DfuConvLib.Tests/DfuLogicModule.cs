using Autofac;
using DfuConvLib.Parts;

namespace DfuConvLib.Tests {
    public class DfuLogicModule : Module {
        protected override void Load(ContainerBuilder builder) {
            var dfuConvLib = typeof(Dfu).Assembly;
            builder.RegisterAssemblyTypes(dfuConvLib)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}
