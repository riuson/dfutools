using DfuToolGui.Controls.Main;
using DfuToolGui.Startup;
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

            var window = this._bootstrapper.Resolve<MainWindow>();
            window.Closed += (sender, args) => this._bootstrapper.Shurdown();

            window.Show();
        }
    }
}
