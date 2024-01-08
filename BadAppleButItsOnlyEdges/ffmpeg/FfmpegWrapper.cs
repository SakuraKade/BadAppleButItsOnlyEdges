using System;
using System.Diagnostics;

namespace BadAppleButItsOnlyEdges.Ffmpeg
{
    internal class FfmpegWrapper
    {
        public static void ExtractAudio(string sourceVideo, string outputAudio)
        {
            string arguments = $"-y -i \"{sourceVideo}\" -f mp3 -vn \"{outputAudio}\"";
            RunFfmpeg(arguments);
        }

        public static void CombineAudioAndVideo(string sourceVideo, string sourceAudio, string outputVideo)
        {
            string arguments = $"-y -i \"{sourceVideo}\" -i \"{sourceAudio}\" -c:v copy -c:a aac \"{outputVideo}\"";
            RunFfmpeg(arguments);
        }

        private static void RunFfmpeg(string arguments)
        {
            const string FFMPEG_PATH = "ffmpeg\\ffmpeg.exe";
            ProcessStartInfo startInfo = new(FFMPEG_PATH, arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            Process process = new()
            {
                StartInfo = startInfo
            };

            process.Start();
            process.WaitForExit();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            if (process.ExitCode != 0)
            {
                throw new Exception($"FFMPEG exited with code {process.ExitCode}\nOutput:\n{output}\nError:\n{error}");
            }

            process.Close();
        }
    }
}
