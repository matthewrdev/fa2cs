using System;
using System.IO;
using System.Threading.Tasks;
using fa2cs.Helpers;

namespace fa2cs
{
    class MainClass
    {
        public const string FontAwesomeIconsFileName = "FontAwesomeIcons.cs";

        public const string FontAwesomeIconsAssemblyFileName = "FontAwesome.IconCodes.dll";

        public const string FontAwesomeIconsAssemblyDocsFileName = "FontAwesome.IconCodes.xml";

        public static async Task Main(string[] args)
        {
            var outputPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            outputPath = Path.Combine(outputPath, "fa2cs");

            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }

            Directory.CreateDirectory(outputPath);

            var downloader = new FontAwesomeDownloader();
            var writer = new CodeWriter();

            var result = await downloader.DownloadIconCodes();

            var code = writer.Write(result);

            var codeFilePath = Path.Combine(outputPath, FontAwesomeIconsFileName);

            File.WriteAllText(codeFilePath, code);
            AssemblyEmitter.EmitAssembly(code, outputPath);

            OpenFileHelper.OpenAndSelect(outputPath);
        }
    }
}
