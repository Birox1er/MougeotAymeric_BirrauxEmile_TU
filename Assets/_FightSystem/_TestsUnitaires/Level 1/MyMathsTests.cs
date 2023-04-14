using _2023_GC_A2_Partiel_POO.Level_1;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_1
{
    internal class MyMathsTests
    {
        [Test]
        [TestCase(1, 1)]
        [TestCase(-11, 11)]
        [TestCase(0, 0)]
        public void AbsoluteTest(int input, int expected)
        {
            var result = MyMath.Abs(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(5, 1, 10, 5)]
        [TestCase(50, 1, 10, 10)]
        [TestCase(-50, 1, 10, 1)]
        public void ClampTest(int input, int min, int max, int expected)
        {
            var result = MyMath.Clamp(input, min, max);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(1f, 1)]
        [TestCase(2.2f, 2)]
        [TestCase(3.5f, 3)]
        [TestCase(4.6f, 4)]
        [TestCase(5.9f, 5)]
        public void FloorTest(float input, int expected)
        {
            var result = MyMath.Floor(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(1f, 1)]
        [TestCase(2.2f, 3)]
        [TestCase(3.5f, 4)]
        [TestCase(4.6f, 5)]
        [TestCase(5.9f, 6)]
        public void CeilTest(float input, int expected)
        {
            var result = MyMath.Ceil(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(1f, 1)]
        [TestCase(2.2f, 2)]
        [TestCase(3.49f, 3)]
        [TestCase(3.50f, 4)]// la bascule se fait ici
        [TestCase(4.6f, 5)]
        [TestCase(5.9f, 6)]
        public void RoundTest(float input, int expected)
        {
            var result = MyMath.Round(input);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(new[] { 1, 2, 3 }, 2, 2)]
        [TestCase(new[] { 42, 24, 56, 98, 01, 56 }, 46.1f, 46.2f)]  // 46,1666666...
        public void AverageTest(int[] input, float expectedMin, float expectedMax)
        {
            var result = MyMath.CalculateAverage(input);
            Assert.That(result, Is.InRange(expectedMin, expectedMax)); 
        }
        [Test]
        [TestCase(null)]
        public void AverageTestNullCheck(int[] input)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = MyMath.CalculateAverage(input);
            });
        }

        [Test]
        [TestCase(new int[] { })]
        public void AverageTestEmptyInput(int[] input)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var result = MyMath.CalculateAverage(input);
            });
        }


    }
}
