using NUnit.Framework;

namespace DotNetFlow.Specifications.Infrastructure
{
    public sealed class IntegrationAttribute : CategoryAttribute
    {
        public IntegrationAttribute() : base("Integration")
        {
        }
    }
}