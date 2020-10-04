using Autofac;

namespace DfuToolGui.Controls {
    public class Registration : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<MainWindow>()
                .AsSelf();
        }
    }
}
