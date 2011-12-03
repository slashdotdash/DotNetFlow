using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DotNetFlow.Core.Web
{
    /// <summary>
    /// Create a URL slug from a given string
    /// </summary>
    /// <example>
    /// "This is an Example" => "this-is-an-example"
    /// </example>
    /// <see cref="http://predicatet.blogspot.com/2009/04/improved-c-slug-generator-or-how-to.html"/>
    public sealed class Slugify
    {        
        private readonly string _separator;        

        private readonly IDictionary<string, string> _replacements;

        public Slugify(string separator = "-")
        {
            if (!new [] { "-", "_", "+" }.Contains(separator))
                throw new ArgumentOutOfRangeException("separator");

            _separator = separator;
            _replacements = new Dictionary<string, string>();
        }
        
        public string GenerateSlug(string source)
        {
            var str = RemoveAccent(source).ToLower();

            str = SubstituteReplacements(str);
            str = StripApostrophes(str);
            str = ReplaceNonSlugCharsWithSeparator(str);
            str = RemoveDuplicateSeparator(str);

            str = TrimPreceedingAndTrailingSeparator(str);
            
            return str;
        }

        private string SubstituteReplacements(string input)
        {
            return _replacements.Aggregate(input, RegexReplacement);            
        }

        private static string RegexReplacement(string input, KeyValuePair<string, string> replacement)
        {
            var searchFor = string.Format(@"^{0}\s|\s{0}\s|\s{0}$", Regex.Escape(replacement.Key));  // Ensure we escape search term in case it includes regex special chars

            return Regex.Replace(input, searchFor, string.Concat(" ", replacement.Value, " "), RegexOptions.IgnoreCase);
        }

        private static string StripApostrophes(string input)
        {
            return input.Replace("'", string.Empty);
        }

        /// <summary>
        /// Replace non-slug characters with the separator (default is a hyphen "-")
        /// </summary>
        private string ReplaceNonSlugCharsWithSeparator(string input)
        {
            return Regex.Replace(input, @"[^a-z0-9\-_\+]", _separator);
        }
        
        private string RemoveDuplicateSeparator(string input)
        {
            return Regex.Replace(input, string.Concat(Regex.Escape(_separator), "{2,}"), _separator).Trim();               
        }

        private string TrimPreceedingAndTrailingSeparator(string input)
        {
            return Regex.Replace(input, string.Format("^{0}|{0}$", Regex.Escape(_separator)), string.Empty);
        }

        public string RemoveAccent(string input)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(input);
            return Encoding.ASCII.GetString(bytes);
        }


        /// <summary>
        /// Replace all occurences of the given string with the replacement (e.g. & => and)
        /// </summary>       
        /// <returns>Itself to allow method chaining</returns>
        public Slugify WithReplacement(string find, string replacement)
        {
            _replacements.Add(find, replacement);
            return this;
        }
    }
}