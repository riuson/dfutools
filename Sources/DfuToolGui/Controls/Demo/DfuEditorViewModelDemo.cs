using DfuLib.Interfaces;

namespace DfuToolGui.Controls.Demo {
    internal class DfuEditorViewModelDemo {
        public DfuEditorViewModelDemo() => this.Dfu = new DfuDemo();

        public IDfu Dfu { get; }
    }
}
