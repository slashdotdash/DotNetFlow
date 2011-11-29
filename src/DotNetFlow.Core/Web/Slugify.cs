using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

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
        private readonly string _source;
        private readonly string _separator;

        private readonly IDictionary<string, string> _replacements = new Dictionary<string, string>
        {
            { "&", "and" },
            { "c#", "c-sharp" },           
        };

        public Slugify(string source, string separator = "-")
        {
            _source = source;
            _separator = separator;
        }

        public string GenerateSlug()
        {
            var str = RemoveAccent(_source).ToLower();

            str = _replacements.Aggregate(str, (current, pair) => current.Replace(pair.Key, pair.Value));

            str = Regex.Replace(str, @"[^a-z0-9\s-]", " "); // Strip out any invalid chars          
            str = Regex.Replace(str, @"\s+", " ").Trim();  // Convert multiple spaces into one space
            str = Regex.Replace(str, @"\s", _separator);  // Replace spaces with the separator char (default is a hyphen "-")

            return str;
        }

        public string Clean()
        {
            var input = HttpUtility.HtmlDecode(_source.Replace("&", "and"));
            input = RemoveAccent(input);

            var sb = new StringBuilder(Regex.Replace(input, @"[^\w\ ]", "").Trim());
            sb.Replace("  ", " ").Replace(" ", "-");

            return sb.ToString().ToLower();
        }

        public string RemoveAccent(string input)
        {
            var bytes = Encoding.GetEncoding("Cyrillic").GetBytes(input);
            return Encoding.ASCII.GetString(bytes);
        }
    }
}