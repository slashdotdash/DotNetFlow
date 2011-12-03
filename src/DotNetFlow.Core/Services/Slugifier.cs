using System;
using DotNetFlow.Core.ReadModel.Queries;
using DotNetFlow.Core.Web;

namespace DotNetFlow.Core.Services
{
    public sealed class Slugifier : IGenerateUrlSlug
    {
        private readonly IFindExistingUrlSlug _existingUrlSlugs;
        private readonly Slugify _slugify;

        public Slugifier(IFindExistingUrlSlug existingUrlSlugs)
        {
            _existingUrlSlugs = existingUrlSlugs;
            
            _slugify = new Slugify()
                .WithReplacement("&", "and")
                .WithReplacement("C#", "c sharp")
                .WithReplacement("ASP.NET", "asp dot net")
                .WithReplacement(".NET", "dot net");
        }

        public string Slugify(string input)
        {
            var slug = _slugify.GenerateSlug(input);
            var index = 1;

            while (_existingUrlSlugs.Exists(slug))
            {
                if (index > 1000) throw new Exception("Took over 1,000 attempts to generate a unique URL slug, aborting");

                index++;
                slug = _slugify.GenerateSlug(string.Concat(input, " ", index));
            }

            return slug;
        }
    }
}