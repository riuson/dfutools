using DfuToolGui.Controls.Main;
using DfuToolGui.Startup;
using ReactiveUI;
using System.Windows;

namespace DfuToolGui {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App {
        private readonly Bootstrapper _bootstrapper;

        public App() => this._bootstrapper = new Bootstrapper();

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            this._bootstrapper.Prepare(this);
            this._bootstrapper.Build();

            var viewModel = this._bootstrapper.Resolve<MainViewModel>();
            var view = this._bootstrapper.Resolve<IViewFor<MainViewModel>>();
            view.ViewModel = viewModel;

            if (view is MainWindow window) {
                window.Closed += (sender, args) => this._bootstrapper.Shurdown();
                window.Show();
            }
        }
    }
}
