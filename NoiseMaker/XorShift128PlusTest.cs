using Xunit;

namespace NoiseMaker {
    public class XorShift128PlusTest {
        private readonly XorShift128Plus rng = new XorShift128Plus(1, 1477776990746309507);

        [Fact]
        public void NextULong() {
            var expected = new ulong[] {
                2955553959480678844,
                4645858385424645336,
                4758446761745768457,
                12856044177209410959,
                9478336499300815185,
                14479017593502201085,
                12345021472730624013,
                541410025137271064,
                10053882551125505274,
                14533654202640345298,
            };

            var actual = new ulong[10];
            for (int i = 0; i < actual.Length; ++i) {
                actual[i] = rng.NextULong();
            }

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(int.MaxValue)]
        public void Next(int n) {
            for (int i = 0; i < 100; ++i) {
                Assert.InRange(rng.Next(n), 0, n - 1);
            }
        }

        [Fact]
        public void NextFloat() {
            for (int i = 0; i < 100; ++i) {
                Assert.InRange(rng.NextFloat(), 0.0f, 1.0f);
            }
        }
    }
}
