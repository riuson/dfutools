using Autofac;

namespace DfuToolGui.Controls.Main {
    public class Registration : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<MainWindow>()
                .AsSelf();
        }
    }
}
