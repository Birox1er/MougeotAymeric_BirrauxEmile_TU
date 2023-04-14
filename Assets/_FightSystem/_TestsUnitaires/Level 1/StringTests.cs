using _2023_GC_A2_Partiel_POO.Level_1;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_1
{
    public class StringTests
    {
        [Test]
        [TestCase("Hello {name}", "Bidule", "Hello Bidule")]
        [TestCase("Hello {name}", "", "Hello ")]
        [TestCase("Hel{name}lo {name}", "Bidule", "HelBidulelo Bidule")]
        [TestCase("{name} come in", "Bidule", "Bidule come in")]
        [TestCase("Hello you", "Bidule", "Hello you")]
        public void BasicReplacement(string baseValue, string replacement, string expected)
        {
            var result = MyString.ReplaceNameTag(baseValue, replacement);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("Hello {name}", null)]
        [TestCase(null, "Bidule")]
        [TestCase(null, null)]
        public void BasicReplacementNullCheck(string baseValue, string replacement)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = MyString.ReplaceNameTag(baseValue, replacement);
            });
        }

        [Test]
        [TestCase("Hello {name}", "{name}", "Bidule", "Hello Bidule")]
        [TestCase("Hello {name}", "{name}", "", "Hello ")]
        [TestCase("Hello {insert name}", "{insert name}", "Bidule", "Hello Bidule")]
        [TestCase("Hello ", "{name}", "Bidule", "Hello ")]
        [TestCase("Hello [nom]", "[name]", "Bidule", "Hello [nom]")]
        [TestCase("oh bah c'est des gamerz", "a", "@", "oh b@h c'est des g@merz")]
        public void BasicReplacementWithGivenTag(string baseValue, string tag, string replacement, string expected)
        {
            var result = MyString.ReplaceGivenTag(baseValue, tag, replacement);

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null, "{name}", "Bidule")]
        [TestCase("Hello {name}", null, "Bidule")]
        [TestCase("Hello {name}", "{name}", null)]
        [TestCase(null, null, "Bidule")]
        [TestCase("Hello {name}", null, null)]
        [TestCase(null, "{name}", null)]
        [TestCase(null, null, null)]
        public void BasicReplacementWithGivenTagNullCheck(string baseValue, string tag, string replacement)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = MyString.ReplaceGivenTag(baseValue, tag, replacement);
            });
        }

        [Test]
        [TestCase("Hello {name}", "", "Bidule")]
        public void BasicReplacementWithGivenTagVerifyEmptyTag(string baseValue, string tag, string replacement)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var result = MyString.ReplaceGivenTag(baseValue, tag, replacement);
            });

        }

        [Test]
        [TestCase(
            "I'm {name}, I'm {age} YO", // baseValue
            new[] { "{name}", "{age}" },  // tags
            new[] { "Bidule", "42" },    // replacement
            "I'm Bidule, I'm 42 YO")]   // expected
        public void MultipleTagReplacement(string baseValue, string[] tags, string[] replacements, string expected)
        {
            var result = MyString.ReplaceMultipleTag(baseValue, tags, replacements);
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        [TestCase(
            null,                       // baseValue
            new[] { "{name}", "{age}" },// tags
            new[] { "Bidule", "42" })]  // replacement
        [TestCase(
            "I'm {name}, I'm {age} YO", // baseValue
            null,                       // tags
            new[] { "Bidule", "42" })]  // replacement
        [TestCase(
            "I'm {name}, I'm {age} YO", // baseValue
            new[] { "{name}", "{age}" },// tags
            null)]                      // replacement
        public void MultipleTagReplacementNullCheck(string baseValue, string[] tags, string[] replacements)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = MyString.ReplaceMultipleTag(baseValue, tags, replacements);
            });
        }

        [TestCase(
            "I'm {name}, I'm {age} YO",     // baseValue
            new[] {  "{age}" },             // tags
            new[] { "Bidule", "42" })]      // replacement
        [TestCase(
            "I'm {name}, I'm {age} YO",     // baseValue
            new[] { "{name}", "{age}" },    // tags
            new[] { "Bidule" })]            // replacement
        public void MultipleTagReplacementMustContainsAsManyTagsAsReplacement(string baseValue, string[] tags, string[] replacements)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var result = MyString.ReplaceMultipleTag(baseValue, tags, replacements);
            });
        }

    }
}
