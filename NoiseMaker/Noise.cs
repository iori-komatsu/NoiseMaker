using Reactive.Bindings;
using System;
using System.ComponentModel;

namespace NoiseMaker {
    class Noise : INotifyPropertyChanged, IDisposable {
        public ReactivePropertySlim<uint> RandomSeed { get; } = new ReactivePropertySlim<uint>(1);

        public void Dispose() {
            RandomSeed.Dispose();
        }

#pragma warning disable CS0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore
    }
}
