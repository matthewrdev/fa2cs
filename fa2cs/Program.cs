using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fa2cs.Helpers;

namespace fa2cs
{
    class MainClass
    {
        public const string Endpoint = "https://fontawesome.com/cheatsheet";

        public static async Task Main(string[] args)
        {
            var exportPath = AssemblyHelper.EntryAssemblyDirectory;
            if (args != null && args.Any())
            {
                exportPath = args.First();
            }

            var outputPath = Path.Combine(exportPath, "fa2cs-output");

            if (Directory.Exists(outputPath))
            {
                Directory.Delete(outputPath, true);
            }

            Directory.CreateDirectory(outputPath);

            var downloader = new FontAwesomeDownloader();
            var codeWriter = new CodeWriter();

            var icons = await downloader.DownloadIconCodes(Endpoint);

            var code = codeWriter.Write(icons);

            File.WriteAllText(Path.Combine(exportPath, "FontAwesomeIcons.cs"), code);

            OpenFileHelper.OpenAndSelect(exportPath);
        }
    }
}
