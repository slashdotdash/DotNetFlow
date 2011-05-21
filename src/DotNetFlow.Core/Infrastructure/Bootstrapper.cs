using StructureMap;

namespace DotNetFlow.Core.Infrastructure
{
    public static class Bootstrapper
    {
        public static void Configure()
        {
            ObjectFactory.Initialize(x => x.AddRegistry<DomainRegistry>());
        }
    }
}