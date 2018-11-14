using System;
using System.IO;
using System.Threading.Tasks;
using fa2cs.Helpers;

namespace fa2cs
{
    class MainClass
    {
        public const string FontAwesomeIconsFileName = "FontAwesomeIcons.cs";

        public static async Task Main(string[] args)
        {
            var downloader = new FontAwesomeDownloader();
            var writer = new CodeWriter();

            var result = await downloader.DownloadIconCodes();

            var code = writer.Write(result);

            var outputFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), FontAwesomeIconsFileName);

            File.WriteAllText(outputFilePath, code);

            OpenFileHelper.OpenAndSelect(outputFilePath);
        }
    }
}
