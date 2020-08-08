using Reactive.Bindings;
using System;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NoiseMaker {
    class NoiseVM : INotifyPropertyChanged, IDisposable {
        public ReactivePropertySlim<uint> RandomSeed { get; } = new ReactivePropertySlim<uint>(1);

        public ReactivePropertySlim<WriteableBitmap> NoiseBitmap { get; }
            = new ReactivePropertySlim<WriteableBitmap>(new WriteableBitmap(256, 256, 96, 96, PixelFormats.Bgr32, null));

        public ReactiveCommand SaveCommand { get; } = new ReactiveCommand();

        public void Dispose() {
            RandomSeed.Dispose();
            NoiseBitmap.Dispose();
            SaveCommand.Dispose();
        }

#pragma warning disable CS0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore
    }
}
