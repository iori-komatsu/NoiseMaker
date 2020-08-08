using Microsoft.Win32;
using System.IO;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;
using System;

namespace NoiseMaker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            ViewModel.RandomSeed
                .Sample(TimeSpan.FromMilliseconds(100))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ => UpdateNoiseImage());

            ViewModel.SaveCommand.Subscribe(_ => DoSave());
        }

        private NoiseVM ViewModel {
            get => (NoiseVM)Resources["ViewModel"];
        }

        private void UpdateNoiseImage() {
            NoisePainter.Paint(ViewModel.NoiseBitmap.Value, ViewModel);
        }

        private void DoSave() {
            var saveFileDialog = new SaveFileDialog {
                Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*",
                DefaultExt = "bmp"
            };
            if (saveFileDialog.ShowDialog() != true) {
                return;
            }

            using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create)) {
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(ViewModel.NoiseBitmap.Value));
                encoder.Save(stream);
            }
        }
    }
}
