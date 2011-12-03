using System.Linq;
using Dapper;
using DotNetFlow.Core.Infrastructure;

namespace DotNetFlow.Core.ReadModel.Queries
{
    public class FindExistingUrlSlugQuery : IFindExistingUrlSlug
    {
        private readonly IUnitOfWork _context;

        public FindExistingUrlSlugQuery(IUnitOfWork context)
        {
            _context = context;
        }

        /// <summary>
        /// Is the given URL slug already assigned?
        /// </summary>
        public bool Exists(string slug)
        {
            return _context.Connection.Query<string>("select Slug from UrlSlugs where Slug = @Slug", new { Slug = slug.ToLower() }, _context.Transaction).Any();
        }
    }
}