using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;
using System.Reactive.Linq;
using System;
using System.Threading;
using System.Reactive;

namespace NoiseMaker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        private readonly WriteableBitmap noiseBitmap = new WriteableBitmap(256, 256, 96, 96, PixelFormats.Bgr32, null);

        public MainWindow() {
            InitializeComponent();

            imNoise.Source = noiseBitmap;
            UpdateNoiseImage();

            Noise.RandomSeed
                .Sample(TimeSpan.FromMilliseconds(100))
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(_ => UpdateNoiseImage());
        }

        private Noise Noise {
            get => (Noise)Resources["ViewModel"];
        }

        private void UpdateNoiseImage() {
            NoisePainter.Paint(noiseBitmap, Noise);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e) {
            var saveFileDialog = new SaveFileDialog {
                Filter = "Bitmap files (*.bmp)|*.bmp|All files (*.*)|*.*",
                DefaultExt = "bmp"
            };
            if (saveFileDialog.ShowDialog() != true) {
                return;
            }

            using (var stream = new FileStream(saveFileDialog.FileName, FileMode.Create)) {
                var encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(noiseBitmap));
                encoder.Save(stream);
            }
        }
    }
}
