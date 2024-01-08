using BadAppleButItsOnlyEdges.Interfaces;
using Emgu.CV;

namespace BadAppleButItsOnlyEdges.Image
{
    internal class EdgeFrameProcessor : IFrameProcessor
    {
        public void ProcessFrame(ref Mat frame)
        {
            Mat edges = new();
            CvInvoke.Canny(frame, edges, threshold1: 100, threshold2: 200);
            frame.Dispose();
            frame = edges;
        }
    }
}
