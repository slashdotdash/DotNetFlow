using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.Web;
using NUnit.Framework;

namespace DotNetFlow.Specifications.Core.Web
{
    //https://github.com/henrik/slugalizer/blob/master/slugalizer.rb
    [TestFixture]
    public class SlugifyFixtures
    {
        [Test]
        public void Should_Transliterate_Characters()
        {
            Assert.AreEqual("internationalization", new Slugify("Iñtërnâtiônàlizætiøn").GenerateSlug());
        }

        [Test]
        public void Should_()
        {
            Assert.AreEqual("", new Slugify("").GenerateSlug());
        }

        //[Test]
        public void Should_Ignore_Valid_Slug()
        {
            Assert.AreEqual("abc-1_2_3", new Slugify("abc-1_2_3").GenerateSlug());
        }

        [Test]
        public void Should_test_Accented_Characters()
        {
            Assert.AreEqual("acegiklnuo", new Slugify("āčēģīķļņūö").GenerateSlug());
        }
     
        //[Test]
        public void Should_Strip_Dashes()
        {
            Assert.AreEqual("i-love-c", new Slugify("I love C--").GenerateSlug());
        }

        //[Test]
        public void Should_Leave_Pluses()
        {
            Assert.AreEqual("i-love-c++", new Slugify("I love C++").GenerateSlug());
        }

        [Test]
        public void Should_Replace_Ampersand_With_And()
        {
            Assert.AreEqual("this-and-that", new Slugify("this & that").GenerateSlug());
        }

        [Test]
        public void Should_Replace_C_Sharp()
        {
            Assert.AreEqual("asp-net-c-sharp", new Slugify("ASP.NET C#").GenerateSlug());
        }

        //[Test]
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
                                "Slug Me_",
                                "Slug Me=",
                                "Slug Me--",
                                "Slug Me---,"
                            };

            foreach (var input in inputs)
                Assert.AreEqual("slug-me", new Slugify(input).GenerateSlug());            
        }
    }
}
