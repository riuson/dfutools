using ReactiveUI;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reflection;
using System.Windows;

namespace DfuToolGui.Controls.Main {
    public class MainViewModel : ReactiveObject {
        public MainViewModel() {
            this.CommandNew = ReactiveCommand.Create(this.New);
            this.CommandExit = ReactiveCommand.Create<Window>(this.Exit);
        }

        public ReactiveCommand<Unit, Unit> CommandNew { get; }
        public ReactiveCommand<Window, Unit> CommandExit { get; }

        private void New() {
            var assembly = Assembly.GetExecutingAssembly();
            var path = assembly.Location;

#if NET_CORE
            var process = new Process {
                StartInfo = {
                    FileName = "dotnet",
                    Arguments = path,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(path),
                    CreateNoWindow = true
                }
            };
#else
            var process = new Process {
                StartInfo = {
                    FileName = path,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(path),
                    CreateNoWindow = false
                }
            };
#endif
            process.Start();
        }

        private async void Exit(Window window) => Application.Current.MainWindow?.Close();
    }
}
