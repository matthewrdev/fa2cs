using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using fa2cs.Helpers;
using QuickType;

namespace fa2cs
{
    [DebuggerDisplay("{Name} - {DotNetName}")]
    public class FontAwesomeIcon
    {
        public FontAwesomeIcon(Datum datum)
        {
            var attr = datum.Attributes;

            Name = attr.Id;
            DotNetName = DotNetNameHelper.ToDotNetName(Name);
            Unicode = attr.Unicode;
            Url = "https://fontawesome.com/icons/" + attr.Id;
            Styles = attr.Styles?.ToList() ?? new List<Style>();
            Membership = attr.Membership;
        }

        public string Name { get; }
        public string Unicode { get; }
        public string Url { get; }
        public IReadOnlyList<Style> Styles { get; }
        public Membership Membership { get; }
        public string DotNetName { get; }

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
