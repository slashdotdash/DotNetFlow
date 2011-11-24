using NUnit.Framework;

namespace DotNetFlow.Specifications.Infrastructure
{
    /// <summary>
    /// Abstract base class for specifcations allowing simple Given/When/Then BDD-style tests
    /// </summary>
    /// <see cref="http://10consulting.com/2011/08/15/simple-bdd-style-unit-tests-with-nunit/"/>
    public abstract class SpecificationBase
    {
        /// <summary>
        /// Execute the abstract Given and When methods once for all the Then test cases
        /// </summary>
        [TestFixtureSetUp]
        public void Setup()
        {
            Given();
            When();
        }

        public abstract void Given();
        public abstract void When();

        [TestFixtureTearDown]
        public void Dispose()
        {
            Finally();
        }

        /// <summary>
        /// Virtual method to allow any resource cleanup as required (disposal)
        /// </summary>
        public virtual void Finally()
        {
        }
    }

    public class SpecificationAttribute : TestFixtureAttribute { }
    public class ThenAttribute : TestAttribute { }
    public class AndAttribute : TestAttribute { }
}
