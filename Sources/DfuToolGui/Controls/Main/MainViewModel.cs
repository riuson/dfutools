using DfuToolGui.Controls.Editor;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace DfuToolGui.Controls.Main {
    public class MainViewModel : ReactiveObject {
        private readonly Func<DfuEditorView> _createView;
        private readonly Func<DfuEditorViewModel> _createViewModel;

        public MainViewModel(
            Func<DfuEditorView> createView,
            Func<DfuEditorViewModel> createViewModel) {
            this._createView = createView;
            this._createViewModel = createViewModel;

            var editorView = this._createView();
            editorView.ViewModel = this._createViewModel();
            this.EditorControl = editorView;
            this.CurrentTime = DateTime.Now;

            this.CommandNew = ReactiveCommand.Create(this.New);
            this.CommandExit = ReactiveCommand.Create<Window>(this.Exit);
        }

        public ReactiveCommand<Unit, Unit> CommandNew { get; }
        public ReactiveCommand<Window, Unit> CommandExit { get; }

        [Reactive] public UserControl EditorControl { get; set; }
        [Reactive] public DateTime CurrentTime { get; set; }

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
