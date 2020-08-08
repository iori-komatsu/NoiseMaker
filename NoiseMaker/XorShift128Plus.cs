namespace NoiseMaker {
    /// <summary>
    /// xorshift128+ に基づく乱数生成器
    /// 
    /// cf. http://vigna.di.unimi.it/ftp/papers/xorshiftplus.pdf
    /// </summary>
    class XorShift128Plus {
        private ulong state0;
        private ulong state1;

        public XorShift128Plus(ulong seed0, ulong seed1 = 1477776990746309507) {
            state0 = seed0;
            state1 = seed1;
        }

        /// <summary>
        /// 0以上n以下の一様分布する乱数を返す
        /// </summary>
        public int Next(int n) {
            return (int)(NextULong() & 0x7fffffffu) % n;
        }

        /// <summary>
        /// 0以上2^64-1以下の一様分布する乱数を返す
        /// </summary>
        public ulong NextULong() {
            ulong s1 = state0;
            ulong s0 = state1;
            state0 = s0;
            s1 ^= s1 << 23;
            s1 ^= s1 >> 17;
            s1 ^= s0;
            s1 ^= s0 >> 26;
            state1 = s1;
            return state0 + state1;
        }

        /// <summary>
        /// 0.0以上1.0以下の一様分布する乱数を返す
        /// </summary>
        public float NextFloat() {
            uint h = (uint)NextULong() & 0x007fffffu | 0x3f800000u;
            unsafe {
                return (*(float*)&h) - 1.0f;
            }
        }
    }
}
