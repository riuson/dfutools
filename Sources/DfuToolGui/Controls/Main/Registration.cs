using Autofac;
using ReactiveUI;

namespace DfuToolGui.Controls.Main {
    public class Registration : Module {
        protected override void Load(ContainerBuilder builder) {
            //builder.RegisterType<MainWindow>()
            //    .AsSelf();
            builder.RegisterType<MainWindow>()
                .As<IViewFor<MainViewModel>>();
            builder.RegisterType<MainViewModel>()
                .AsSelf();
        }
    }
}
