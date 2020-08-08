using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NoiseMaker {
    static class NoisePainter {
        private static void WritePixels(WriteableBitmap bitmap, Func<int, int, Color> plotter) {
            try {
                bitmap.Lock();

                unsafe {
                    IntPtr pBackBuffer = bitmap.BackBuffer;
                    for (int y = 0; y < bitmap.PixelHeight; ++y) {
                        IntPtr pRow = pBackBuffer + y * bitmap.BackBufferStride;
                        for (int x = 0; x < bitmap.PixelWidth; ++x) {
                            IntPtr pPixel = pRow + x * 4;
                            Color c = plotter(x, y);
                            *((int*)pPixel) = c.R << 16 | c.G << 8 | c.B;
                        }
                    }
                }

                bitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.PixelWidth, bitmap.PixelHeight));
            } finally {
                bitmap.Unlock();
            }
        }

        public static void Paint(WriteableBitmap bitmap, Noise noise) {
            var rng = new XorShift128Plus(noise.RandomSeed.Value);
            WritePixels(bitmap, (x, y) => {
                byte r = (byte)rng.Next(256);
                byte g = (byte)rng.Next(256);
                byte b = (byte)rng.Next(256);
                return Color.FromRgb(r, g, b);
            });
        }
    }
}
