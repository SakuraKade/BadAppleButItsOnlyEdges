using BadAppleButItsOnlyEdges.Ffmpeg;
using BadAppleButItsOnlyEdges.Image;
using BadAppleButItsOnlyEdges.Interfaces;
using Emgu.CV;
using System;
using System.Diagnostics;
using System.IO;

internal class Program
{
    const string SOURCE_VIDEO = "BadApple.mp4";
    const string OUTPUT_VIDEO = "BadAppleButItsOnlyEdges.mp4";
    const string OUTPUT_VIDEO_WITH_AUDIO = "BadAppleButItsOnlyEdgesWithAudio.mp4";
    const string OUTPUT_AUDIO = "audio.mp3";

    private static void Main(string[] args)
    {
        try
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            ProcessAndSaveFrames();
            ExtractAndMergeAudio();
            Cleanup();

            stopwatch.Stop();
            Console.WriteLine($"Finished in {stopwatch.ElapsedMilliseconds}ms");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
#if DEBUG
            throw;
#endif
        }
        finally
        {
            Console.WriteLine("Press any key to exit"); Console.ReadKey();
        }
    }

    private static void Cleanup()
    {
        // Cleanup
        Console.WriteLine("Cleaning up...");
        File.Delete(OUTPUT_AUDIO);
        File.Delete(OUTPUT_VIDEO);
        File.Move(OUTPUT_VIDEO_WITH_AUDIO, OUTPUT_VIDEO);
    }

    private static void ExtractAndMergeAudio()
    {
        Console.WriteLine("Extracting audio...");
        FfmpegWrapper.ExtractAudio(SOURCE_VIDEO, OUTPUT_AUDIO);
        Console.WriteLine("Combining audio and video...");
        FfmpegWrapper.CombineAudioAndVideo(OUTPUT_VIDEO, OUTPUT_AUDIO, OUTPUT_VIDEO_WITH_AUDIO);
    }

    private static void ProcessAndSaveFrames()
    {
        int currentFrame = 0;
        using (FileFrameReader reader = new(SOURCE_VIDEO))
        {
            using VideoWriter writer = new(OUTPUT_VIDEO, reader.FrameRate, reader.FrameSize, isColor: false);
            IFrameProcessor processor = new EdgeFrameProcessor();
            Mat? frame = new();
            while (reader.Next(ref frame))
            {
                processor.ProcessFrame(ref frame);
                currentFrame++;
                Console.WriteLine($"Frame {currentFrame}");
                writer.Write(frame);
            }
        }
    }
}