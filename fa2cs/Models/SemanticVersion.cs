using System;
using System.Linq;

namespace fa2cs.Models
{
    public class SemanticVersion : IComparable<SemanticVersion>
    {
        private const string tagDeliminator = "-";

        public int Major { get; }

        public int Minor { get; }

        public int Patch { get; }

        public string Tag { get; }

        public SemanticVersion(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"'{nameof(value)}' cannot be null or whitespace.", nameof(value));
            }

            var parts = value.Split('.');

            Major = TryParse(parts, 0);
            Minor = TryParse(parts, 1);
            Patch = TryParse(parts, 2);

            if (parts.Last().Contains(tagDeliminator))
            {
                Tag = parts.Last().Split(tagDeliminator).Last();
            }
        }

        private int TryParse(string[] parts, int element)
        {
            if (parts is null)
            {
                throw new ArgumentNullException(nameof(parts));
            }

            if (element >= parts.Length)
            {
                return 0;
            }

            var content = parts[element];

            if (content.Contains(tagDeliminator))
            {
                content = content.Split(tagDeliminator).First();
            }

            return int.Parse(content);
        }

        public override string ToString()
        {
            var content = $"{Major}.{Minor}.{Patch}";

            if (!string.IsNullOrEmpty(Tag))
            {
                content += tagDeliminator + Tag;
            }

            return content;
        }

        public int CompareTo(SemanticVersion that)
        {
            if (that == null) return 1;

            if (this.Major < that.Major)
            {
                return -1;
            }

            if (this.Major > that.Major)
            {
                return 1;
            }

            if (this.Minor < that.Minor)
            {
                return -1;
            }
            if (this.Minor > that.Minor)
            {
                return 1;
            }

            if (this.Patch < that.Patch)
            {
                return -1;
            }

            if (this.Patch > that.Patch)
            {
                return 1;
            }

            if(string.IsNullOrEmpty(this.Tag) && string.IsNullOrEmpty(that.Tag))
            {
                return 0;
            }

            if (string.IsNullOrEmpty(this.Tag))
            {
                return 1;
            }

            if (string.IsNullOrEmpty(that.Tag))
            {
                return -1;
            }

            return this.Tag.CompareTo(that.Tag);
        }
    }
}