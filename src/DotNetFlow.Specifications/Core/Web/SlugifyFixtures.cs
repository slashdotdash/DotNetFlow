using DotNetFlow.Core.Web;
using NUnit.Framework;

namespace DotNetFlow.Specifications.Core.Web
{
    /// <summary>
    /// Test slugification (inspired by slugalizer Ruby gem)
    /// </summary>
    /// <see cref="https://github.com/henrik/slugalizer/blob/master/slugalizer.rb"/>
    [TestFixture]
    public class SlugifyFixtures
    {
        [Test]
        public void Should_Slugify_Given_String()
        {
            AssertSlugification("this-is-an-example", "This is an Example");
        }

        [Test]
        public void Should_Trim_Whitespace()
        {
            AssertSlugification("strip-preceeding-whitespace", " strip preceeding whitespace");
            AssertSlugification("strip-trailing-whitespace", "strip trailing whitespace   ");
            AssertSlugification("strip-inner-whitespace-too", "  strip   inner   whitespace  too   ");
        }

        [Test]
        public void Should_Ignore_Valid_Slug()
        {
            AssertSlugification("abc-1_2_3", "abc-1_2_3");
        }

        [Test]
        public void Should_Combine_Separators()
        {
            AssertSlugification("a-b", "a - b");
            AssertSlugification("a-b", "a--b");
        }

        [Test]
        public void Should_Transliterate_Characters()
        {
            AssertSlugification("internationalization", "Iñtërnâtiônàlizætiøn");
        }        

        [Test]
        public void Should_test_Accented_Characters()
        {
            AssertSlugification("acegiklnuo", "āčēģīķļņūö");
        }
     
        [Test]
        public void Should_Strip_Trailing_Dashes()
        {
            AssertSlugification("without-trailing-dash", "without-trailing-dash--");
        }

        [Test]
        public void Should_Strip_Preceeding_Dashes()
        {
            AssertSlugification("without-preceeding-dash", "--without-preceeding-dash");
        }

        [Test]
        public void Should_Strip_Apostrophes()
        {
            AssertSlugification("users-comment", "User's Comment");
        }

        [Test]
        public void Should_Replace_Ampersand_With_And()
        {
            var slugify = new Slugify();
            slugify.WithReplacement("&", "and");

            Assert.AreEqual("this-and-that", slugify.GenerateSlug("this & that"));            
        }

        /// <summary>
        /// Test domain-specific word replacements
        /// </summary>
        [Test]
        public void Should_Replace_AspNet_And_C_Sharp()
        {
            var slugify = new Slugify();
            slugify.WithReplacement("C#", "c sharp");
            slugify.WithReplacement("ASP.NET", "asp dot net");

            Assert.AreEqual("asp-dot-net-c-sharp", slugify.GenerateSlug("ASP.NET C#"));    
        }

        [Test]
        public void Should_Replace_dotNetFramework()
        {
            var slugify = new Slugify();
            slugify.WithReplacement(".NET", "dot net");

            Assert.AreEqual("microsoft-dot-net-framework", slugify.GenerateSlug("Microsoft .NET Framework"));
        }        

        [Test]
        public void Should_Remove_Trailing_Punctuation()
        {
            var inputs = new[]
                            {
                                "Slug Me ",
                                "Slug Me,",
                                "Slug Me.",
                                "Slug Me/",
                                "Slug Me\\",
                                "Slug Me-",
                                "Slug Me",
                                "Slug Me=",
                                "Slug Me--",
                                "Slug Me---,"
                            };

            foreach (var input in inputs)
                AssertSlugification("slug-me", input);
        }

        private static void AssertSlugification(string expected, string source)
        {
            Assert.AreEqual(expected, new Slugify().GenerateSlug(source));
        }
    }
}
