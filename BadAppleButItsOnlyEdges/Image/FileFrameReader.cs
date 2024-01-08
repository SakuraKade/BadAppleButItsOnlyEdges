using BadAppleButItsOnlyEdges.Interfaces;
using Emgu.CV;
using System;
using System.Drawing;

namespace BadAppleButItsOnlyEdges.Image
{
    internal class FileFrameReader : IFrameEnumerator, IDisposable
    {
        private bool _disposed;
        private readonly string _sourceVideoPath;
        private readonly VideoCapture _videoCapture;

        public int FrameRate { get => 30; } // BadApple.mp4 is 60fps

        private readonly Size _frameSize;
        public Size FrameSize { get => _frameSize; }

        public FileFrameReader(string sourceVideoPath)
        {
            _sourceVideoPath = sourceVideoPath;
            _videoCapture = new VideoCapture(_sourceVideoPath);
            _frameSize = new Size(_videoCapture.Width, _videoCapture.Height);
        }

        public bool Next(ref Mat? frame)
        {
            ObjectDisposedException.ThrowIf(_disposed, nameof(FileFrameReader));

            if (!_videoCapture.Read(frame))
            {
                return false;
            }

            return true;
        }

        public void Dispose()
        {
            _disposed = true;
            _videoCapture?.Dispose();
        }
    }
}
