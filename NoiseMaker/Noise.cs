using System.ComponentModel;

namespace NoiseMaker {
    class Noise {
        public event PropertyChangedEventHandler PropertyChanged;

        private uint randomSeed = 1;

        public uint RandomSeed {
            get => randomSeed;
            set {
                if (randomSeed != value) {
                    randomSeed = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RandomSeed)));
                }
            }
        }
    }
}
