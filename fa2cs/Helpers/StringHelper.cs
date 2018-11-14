using System;
using System.IO;
using System.Text;

namespace fa2cs.Helpers
{
    /// <summary>
    /// A collection of helper methods for working with strings.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Converts the first character in the string to its upper case variant.
        /// </summary>
        /// <returns>The char to upper.</returns>
        /// <param name="input">Input.</param>
        public static string FirstCharToUpper(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if (input.Length == 1)
            {
                return input[0].ToString().ToUpper();
            }

            return input[0].ToString().ToUpper() + input.Substring(1);
        }

        /// <summary>
        /// Converts the first character in the string to its lower case variant.
        /// </summary>
        /// <returns>The char to lower.</returns>
        /// <param name="input">Input.</param>
        public static string FirstCharToLower(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            if (input.Length == 1)
            {
                return input[0].ToString().ToLower();
            }

            return input[0].ToString().ToLower() + input.Substring(1);
        }

        /// <summary>
        /// Finds all uppercase characters in the <paramref name="input"/> string and inserts a space between them.
        /// </summary>
        /// <returns>The upper letters by space.</returns>
        /// <param name="input">Input.</param>
        public static string SeparateUpperLettersBySpace(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            string output = "";

            foreach (var s in input)
            {
                if (char.IsUpper(s) && !string.IsNullOrEmpty(output))
                {
                    output += " ";
                }
                output += s;
            }

            return output;
        }

        /// <summary>
        /// Converts the provided string to a stream.
        /// </summary>
        /// <returns>The stream.</returns>
        /// <param name="s">S.</param>
        public static Stream AsStream(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        /// <summary>
        /// Reads a string from the provided stream.
        /// </summary>
        /// <returns>The stream.</returns>
        /// <param name="stream">Stream.</param>
        public static string FromStream(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
