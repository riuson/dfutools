using Autofac;

namespace DfuToolGui.Controls.Editor {
    public class Registration : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.RegisterType<DfuEditorView>()
                .AsSelf();
            builder.RegisterType<DfuEditorViewModel>()
                .AsSelf();
            builder.RegisterType<DfuEditorModel>()
                .AsSelf();
        }
    }
}
