using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fa2cs.Helpers;
using fa2cs.Models;

namespace fa2cs
{
    /// <summary>
    /// Exports a C# class file containing named and documented public properties for all icons in the Font Awesome Pro icon set.
    /// <para/>
    /// To run the code generator, you will need:
    /// <list type="bullet">
    /// <item>A paid Font Awesome subscription.</item>
    /// <item>To download the 'Font Awesome Pro for the web' archive data: https://fontawesome.com/download </item>
    /// <item>To place the 'metadata/icons.json' onto your Desktop and name it 'icons.json'</item>
    /// </list>
    /// </summary>
    class MainClass
    {
        public static void Main(string[] args)
        {
            var exportPath = new DirectoryInfo(AssemblyHelper.EntryAssemblyDirectory) // $repository_path$/fa2cs/bin/$release$/$runtime$/
                                 .Parent                                              // $repository_path$/fa2cs/bin/$release$/
                                 .Parent                                              // $repository_path$/fa2cs/bin/
                                 .Parent                                              // $repository_path$/fa2cs/
                                 .Parent                                              // $repository_path$/
                                 .FullName;

            var importPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "icons.json");
            if (!File.Exists(importPath))
            {
                throw new InvalidOperationException($"fa2cs cannot generate the C# class as the 'icons.json' metadata was not found on the desktop.\n" +
                                                    $"Expected location is: {importPath}");
            }    

            var parser = new MetaDataParser();
            var icons = parser.Parse(importPath, out var version);

            WriteCode(exportPath, icons, version);

            WriteReadme(exportPath, version);

            OpenFileHelper.OpenAndSelect(exportPath);
        }

        private static void WriteCode(string exportPath, IReadOnlyList<Icon> icons, SemanticVersion version)
        {
            var codeWriter = new CodeWriter();
            var code = codeWriter.Write(icons, version);

            File.WriteAllText(Path.Combine(exportPath, "FontAwesomeIcons.cs"), code);
        }

        private static void WriteReadme(string exportPath, SemanticVersion version)
        {
            var readmeWriter = new ReadmeWriter();

            var readme = readmeWriter.Write(version.ToString());

            File.WriteAllText(Path.Combine(exportPath, "Readme.md"), readme);
        }
    }
}
