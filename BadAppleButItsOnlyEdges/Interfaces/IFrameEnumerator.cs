using Emgu.CV;
using System.Drawing;

namespace BadAppleButItsOnlyEdges.Interfaces
{
    internal interface IFrameEnumerator
    {
        public int FrameRate { get; }
        public Size FrameSize { get; }

        bool Next(ref Mat? frame);
    }
}
