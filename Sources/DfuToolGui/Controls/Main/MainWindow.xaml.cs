using ReactiveUI;
using System.Reactive.Disposables;

namespace DfuToolGui.Controls.Main {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<MainViewModel> {
        private MainViewModel _viewModel;

        public MainWindow() {
            this.InitializeComponent();

            this.WhenActivated(disposables => {
                this.BindCommand(
                        this.ViewModel,
                        vm => vm.CommandNew,
                        v => v.MenuItemNew)
                    .DisposeWith(disposables);
                this.BindCommand(
                        this.ViewModel,
                        vm => vm.CommandExit,
                        v => v.MenuItemExit)
                    .DisposeWith(disposables);
                this.BindCommand(
                        this.ViewModel,
                        vm => vm.CommandSave,
                        v => v.MenuItemSave)
                    .DisposeWith(disposables);
                this.BindCommand(
                        this.ViewModel,
                        vm => vm.CommandSaveAs,
                        v => v.MenuItemSaveAs)
                    .DisposeWith(disposables);
                this.BindCommand(
                        this.ViewModel,
                        vm => vm.CommandOpen,
                        v => v.MenuItemOpen)
                    .DisposeWith(disposables);
            });
        }

        object IViewFor.ViewModel {
            get => this.ViewModel;
            set => this.ViewModel = value as MainViewModel;
        }

        public MainViewModel ViewModel {
            get => this._viewModel;
            set {
                this._viewModel = value;
                this.DataContext = value;
            }
        }
    }
}
