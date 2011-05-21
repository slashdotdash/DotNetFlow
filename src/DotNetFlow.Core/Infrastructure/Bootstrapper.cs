using Ncqrs;

namespace DotNetFlow.Core.Infrastructure
{
    public static class Bootstrapper
    {
        public static void Configure()
        {
            var config = new StructureMapConfiguration(cfg => cfg.AddRegistry<DomainRegistry>());
            NcqrsEnvironment.Configure(config);
        }
    }
}