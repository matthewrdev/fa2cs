using System;
using System.Collections.Generic;
using fa2cs.Helpers;
using fa2cs.Models;

namespace fa2cs
{
    public class CodeWriter
    {
        public string Write(IReadOnlyList<Icon> icons, SemanticVersion version)
        {
            Console.Write("Generating C# code...");

            var classTemplate = ResourcesHelper.ReadResourceContent("ClassTemplate.txt");
            var propertyTemplate = ResourcesHelper.ReadResourceContent("PropertyTemplate.txt");

            var properties = new List<string>();

            foreach (var icon in icons)
            {
                var property = propertyTemplate.Replace("$link$", icon.Url)
                                       .Replace("$name$", icon.Id)
                                       .Replace("$code$", icon.Unicode)
                                       .Replace("$dotnet_name$", icon.DotNetName)
                                       .Replace("$introduced_version$", icon.IntroducedVersion)
                                       .Replace("$last_modified_version$", icon.LastModifiedVersion)
                                       .Replace("$styles$", icon.StylesSummary);

                properties.Add(property);
            }

            var separator = Environment.NewLine + Environment.NewLine;
            var code = string.Join(separator, properties);

            return classTemplate.Replace("$properties$", code)
                                .Replace("$version$", version.ToString());
        }
    }
}
