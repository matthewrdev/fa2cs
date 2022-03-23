using System;
using System.Collections.Generic;
using System.Linq;
using fa2cs.Helpers;

namespace fa2cs.Models
{
	public class Icon
	{
        public Icon(string id,
                    string unicode,
                    IReadOnlyList<string> versions,
                    IReadOnlyList<Style> freeStyles,
                    IReadOnlyList<Style> proStyles)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException($"'{nameof(id)}' cannot be null or empty.", nameof(id));
            }

            if (string.IsNullOrEmpty(unicode))
            {
                throw new ArgumentException($"'{nameof(unicode)}' cannot be null or empty.", nameof(unicode));
            }

            Id = id;
            Unicode = unicode;
            Versions = versions ?? throw new ArgumentNullException(nameof(versions));
            FreeStyles = freeStyles ?? throw new ArgumentNullException(nameof(freeStyles));
            ProStyles = proStyles ?? throw new ArgumentNullException(nameof(proStyles));
        }

		public string Id { get; }

        public string Url => "https://fontawesome.com/icons/" + Id;

		public string Unicode { get; }

        // Assumes that versions processed from icons.json is correctly sorted.
        public IReadOnlyList<string> Versions { get; }

        public string IntroducedVersion => Versions.First();

        public string LastModifiedVersion => Versions.Last();

        public IReadOnlyList<Style> FreeStyles { get; }

        public IReadOnlyList<Style> ProStyles { get; }

        public string DotNetName => DotNetNameHelper.ToDotNetName(Id);

        public string StylesSummary
        {
            get
            {
                List<Style> all = new List<Style>();
                all.AddRange(FreeStyles);
                all.AddRange(ProStyles);
                var distinct = all.Distinct().ToList();

                distinct.Sort();

                var styles = distinct.Select(d => d.ToString() + ((ProStyles.Contains(d) && !FreeStyles.Contains(d)) ? " (Pro)" : ""));

                return string.Join(", ", styles);
            }
        }
    }
}

