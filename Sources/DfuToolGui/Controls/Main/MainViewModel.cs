using DfuToolGui.Controls.Editor;
using Microsoft.Win32;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace DfuToolGui.Controls.Main {
    public class MainViewModel : ReactiveObject {
        private readonly Func<DfuEditorView> _createView;
        private readonly Func<DfuEditorViewModel> _createViewModel;
        private readonly DfuEditorViewModel _editorViewModel;
        private readonly ObservableAsPropertyHelper<string> _windowTitle;

        public MainViewModel(
            Func<DfuEditorView> createView,
            Func<DfuEditorViewModel> createViewModel) {
            this._createView = createView;
            this._createViewModel = createViewModel;

            var editorView = this._createView();
            this._editorViewModel = this._createViewModel();
            editorView.ViewModel = this._editorViewModel;
            this.EditorControl = editorView;

            this.FileName = string.Empty;

            this.CommandNew = ReactiveCommand.Create(this.New);
            this.CommandExit = ReactiveCommand.Create<Window>(this.Exit);
            this.CommandSave = ReactiveCommand.Create(this.Save);
            this.CommandSaveAs = ReactiveCommand.Create(this.SaveAs);
            this.CommandOpen = ReactiveCommand.Create(this.Open);

            this._windowTitle = this.WhenAnyValue(vm => vm.FileName)
                .Select(x => string.IsNullOrEmpty(x) ? "DfuToolGui" : $"DfuToolGui - {x}")
                .ToProperty(this, vm => vm.WindowTitle);
        }

        public ReactiveCommand<Unit, Unit> CommandNew { get; }
        public ReactiveCommand<Window, Unit> CommandExit { get; }
        public ReactiveCommand<Unit, Unit> CommandSave { get; }
        public ReactiveCommand<Unit, Unit> CommandSaveAs { get; }
        public ReactiveCommand<Unit, Unit> CommandOpen { get; }

        [Reactive] public UserControl EditorControl { get; set; }
        [Reactive] public string FileName { get; set; }
        public string WindowTitle => this._windowTitle.Value;

        private void New() => this.RunNewInstance(string.Empty);

        private void Exit(Window window) => Application.Current.MainWindow?.Close();

        private void Save() {
            if (string.IsNullOrEmpty(this.FileName)) {
                this.SaveAs();
            } else {
                this._editorViewModel.Save(this.FileName);
            }
        }

        private void SaveAs() {
            var dialog = new SaveFileDialog {
                AddExtension = true,
                CheckPathExists = true,
                DefaultExt = ".dfu",
                Filter = "Device Firmware Upgrade (*.dfu)|*.dfu",
                FilterIndex = 1,
                OverwritePrompt = true,
                Title = "Save DFU"
            };

            if (dialog.ShowDialog() == true) {
                this._editorViewModel.Save(dialog.FileName);
                this.FileName = dialog.FileName;
            }
        }

        private void Open() {
            var dialog = new OpenFileDialog {
                AddExtension = true,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".dfu",
                Filter = "Device Firmware Upgrade (*.dfu)|*.dfu",
                FilterIndex = 1,
                Title = "Open DFU"
            };

            if (dialog.ShowDialog() == true) {
                this.RunNewInstance(dialog.FileName);
            }
        }

        private void RunNewInstance(string argument) {
            var assembly = Assembly.GetExecutingAssembly();
            var path = assembly.Location;

#if NET_CORE
            var process = new Process {
                StartInfo = {
                    FileName = "dotnet",
                    Arguments = path + " " + argument,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(path),
                    CreateNoWindow = true
                }
            };
#else
            var process = new Process {
                StartInfo = {
                    FileName = path,
                    Arguments = argument,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetDirectoryName(path),
                    CreateNoWindow = false
                }
            };
#endif
            process.Start();
        }

        public void Load(string filename) {
            this._editorViewModel.Load(filename);
            this.FileName = filename;
        }
    }
}
