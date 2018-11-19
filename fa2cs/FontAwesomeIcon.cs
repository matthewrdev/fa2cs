using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using fa2cs.Helpers;
using QuickType;
namespace fa2cs
{
    public class FontAwesomeIcon
    {
        public static readonly IReadOnlyDictionary<string, string> DotNetNameMap = new Dictionary<string, string>()
        {
            { "500px", "FiveHundredPX"},
        };

        public FontAwesomeIcon(Datum datum)
        {
            var attr = datum.Attributes;

            Name = attr.Id;
            Unicode = attr.Unicode;
            Url = "https://fontawesome.com/icons/" + attr.Id;
            Styles = attr.Styles?.ToList() ?? new List<Style>();
            Membership = attr.Membership;
        }

        public string Name { get; }
        public string Unicode { get; }
        public string Url { get; }
        public List<Style> Styles { get; }
        public Membership Membership { get; }

        public string DotNetName
        {
            get
            {
                if (DotNetNameMap.ContainsKey(Name))
                {
                    return DotNetNameMap[Name];
                }

                var split = Name.Split('-');

                string dotNetName = "";
                foreach (var s in split)
                {
                    dotNetName += StringHelper.FirstCharToUpper(s);
                }

                return dotNetName;
            }
        }

        public string StylesSummary
        {
            get
            {
                List<Style> all = new List<Style>();
                all.AddRange(Styles);
                var distinct = all.Distinct().ToList();

                distinct.Sort();

                var styles = distinct.Select(d => d.ToString() + ((Membership.Pro.Contains(d) && !Membership.Free.Contains(d)) ? " (Pro)" : ""));

                return string.Join(", ", styles);
            }
        }
    }
}
