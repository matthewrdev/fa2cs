using System;
using System.Collections.Generic;
using fa2cs.Helpers;
using fa2cs.Models;

namespace fa2cs
{
    public class ReadmeWriter
    {
        public string Write(string fontAwesomeVersion)
        {
            Console.Write("Generating repository readme...");

            var readmeTemplate = ResourcesHelper.ReadResourceContent("Readme.txt");

            var now = DateTimeOffset.UtcNow;

            return readmeTemplate.Replace("$latest_version$", fontAwesomeVersion)
                                 .Replace("$exported_date_time$", now.ToString("F"))
                                 .Replace("$exported_timezone$", "UTC");
        }
    }
}
