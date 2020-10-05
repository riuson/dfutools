using ReactiveUI;
using System.Reactive;
using System.Windows;

namespace DfuToolGui.Controls.Main {
    public class MainViewModel : ReactiveObject {
        public MainViewModel() => this.CommandExit = ReactiveCommand.Create<Window>(this.Exit);

        public ReactiveCommand<Window, Unit> CommandExit { get; }

        private async void Exit(Window window) => Application.Current.MainWindow?.Close();
    }
}
