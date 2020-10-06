using ReactiveUI;

namespace DfuToolGui.Controls.Editor {
    /// <summary>
    /// Логика взаимодействия для DfuEditorView.xaml
    /// </summary>
    public partial class DfuEditorView : IViewFor<DfuEditorViewModel> {
        private DfuEditorViewModel _viewModel;

        public DfuEditorView() {
            this.InitializeComponent();
        }

        object IViewFor.ViewModel {
            get => this.ViewModel;
            set => this.ViewModel = value as DfuEditorViewModel;
        }

        public DfuEditorViewModel ViewModel {
            get => this._viewModel;
            set {
                this._viewModel = value;
                this.DataContext = value;
            }
        }
    }
}
