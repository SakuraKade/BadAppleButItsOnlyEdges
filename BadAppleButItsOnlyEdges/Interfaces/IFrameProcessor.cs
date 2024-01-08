using Emgu.CV;

namespace BadAppleButItsOnlyEdges.Interfaces
{
    internal interface IFrameProcessor
    {
        void ProcessFrame(ref Mat frame);
    }
}
