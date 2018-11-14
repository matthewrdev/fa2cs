using System;
using System.IO;
using System.Threading.Tasks;
using fa2cs.Helpers;

namespace fa2cs
{
    class MainClass
    {
        public static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new InvalidOperationException("Please provide a file to output");
            }

            var outputFile = args[0];

            var downloader = new FontAwesomeDownloader();
            var writer = new CodeWriter();

            var result = await downloader.DownloadIconCodes();

            var code = writer.Write(result);

            var outputFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), outputFile);

            File.WriteAllText(outputFilePath, code);

            OpenFileHelper.OpenAndSelect(outputFilePath);
        }
    }
}
