using System.Collections.Concurrent;
using DotNetFlow.Core.ReadModel.Queries;

namespace DotNetFlow.Utils
{
    internal sealed class InMemorySlugStore : IFindExistingUrlSlug
    {
        private ConcurrentDictionary<string, string> _slugs;

        public InMemorySlugStore()
        {
            _slugs = new ConcurrentDictionary<string, string>();
        }

        public bool Exists(string slug)
        {
            return !_slugs.TryAdd(slug, slug);
        }
    }
}