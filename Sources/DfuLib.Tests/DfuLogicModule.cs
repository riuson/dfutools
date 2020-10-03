using Autofac;
using DfuLib.Parts;

namespace DfuLib.Tests {
    public class DfuLogicModule : Module {
        protected override void Load(ContainerBuilder builder) {
            var dfuConvLib = typeof(Dfu).Assembly;
            builder.RegisterAssemblyTypes(dfuConvLib)
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}
