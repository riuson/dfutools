using Autofac;
using DfuToolCli.Interfaces;
using System.Reflection;
using Module = Autofac.Module;

namespace DfuToolCli.Tools {
    public class ToolsModule : Module {
        protected override void Load(ContainerBuilder builder) {
            var currentAssembly = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(t => t.IsAssignableTo<IVerbOptions>())
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(t => t.IsAssignableTo<IVerbProcessor>())
                .AsImplementedInterfaces()
                .AsSelf();

            builder.RegisterAssemblyTypes(currentAssembly)
                .Where(t => t.IsAssignableTo<ILink>())
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}
