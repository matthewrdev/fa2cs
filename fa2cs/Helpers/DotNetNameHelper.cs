using System;
using System.Collections.Generic;

namespace fa2cs.Helpers
{
    public static class DotNetNameHelper
    {
        public static readonly IReadOnlyDictionary<string, string> DotNetNameMap = new Dictionary<string, string>()
        {
            { "500px", "FiveHundredPX"},
            { "equals", "Equal"},
        };

        public static string ToDotNetName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }

            if (DotNetNameMap.ContainsKey(name))
            {
                return DotNetNameMap[name];
            }

            var split = name.Split('-');

            string dotNetName = "";
            foreach (var s in split)
            {
                dotNetName += StringHelper.FirstCharToUpper(s);
            }

            return dotNetName;
        }
    }
}
