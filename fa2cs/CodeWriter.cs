using System;
using System.Collections.Generic;
using fa2cs.Helpers;

namespace fa2cs
{
    public class CodeWriter
    {
        const string indent = "    ";

        public string Write(List<FontAwesomeIcon> icons)
        {
            Console.Write("Generating C# code...");

            var classTemplate = ResourcesHelper.ReadResourceContent("ClassTemplate.txt");
            var propertyTemplate = ResourcesHelper.ReadResourceContent("PropertyTemplate.txt");

            List<string> properties = new List<string>();

            foreach (var icon in icons)
            {
                var property = propertyTemplate.Replace("$link$", icon.Url)
                                       .Replace("$name$", icon.Name)
                                       .Replace("$code$", icon.Unicode)
                                       .Replace("$dotnet_name$", icon.DotNetName)
                                       .Replace("$styles$", icon.StylesSummary);

                properties.Add(property);
            }

            var separator = Environment.NewLine + Environment.NewLine;
            var code = string.Join(separator, properties);

            return classTemplate.Replace("$properties$", code);
        }
    }
}
