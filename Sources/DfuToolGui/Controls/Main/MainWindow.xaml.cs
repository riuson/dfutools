using ReactiveUI;
using System.Reactive.Disposables;

namespace DfuToolGui.Controls.Main {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<MainViewModel> {
        public MainWindow() {
            this.InitializeComponent();

            this.WhenActivated(disposables => {
                this.BindCommand(
                        this.ViewModel,
                        vm => vm.CommandExit,
                        v => v.MenuItemExit)
                    .DisposeWith(disposables);
            });
        }

        object IViewFor.ViewModel {
            get => this.ViewModel;
            set => this.ViewModel = value as MainViewModel;
        }

        public MainViewModel ViewModel { get; set; }
    }
}
