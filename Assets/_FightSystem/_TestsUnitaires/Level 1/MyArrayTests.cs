using _2023_GC_A2_Partiel_POO.Level_1;
using NUnit.Framework;
using System;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_1
{

    public interface IRandom
    {

    }


    public class MyArrayTests
    {
        [Test]
        [TestCase(new[] { 1,2,3 }, 12, new[] {1,2,3,12})]
        [TestCase(new int[] { }, 12, new[] {12})]
        public void AddElementInArray(int[] initialArray, int elementToAdd, int[] expected)
        {
            var result = MyArray.AppendElementInArray(initialArray, elementToAdd);
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        [TestCase(null, 12)]
        public void AddElementInArrayNullCheck(int[] initialArray, int elementToAdd)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = MyArray.AppendElementInArray(initialArray, elementToAdd);
            });
        }

        [Test]
        [TestCase(new[] { 1, 2, 3 }, new[] {4,5,6}, new[] { 1,2,3,4,5,6})]
        [TestCase(new int[] {}, new[] { 4,5,6}, new[] { 4,5,6})]
        [TestCase(new[] { 1, 2, 3 }, new int[] {}, new[] { 1,2,3})]
        public void ConcatArrays(int[] initialArray, int[] elementToAdd, int[] expected)
        {
            var result = MyArray.ConcatElementsInArray(initialArray, elementToAdd);
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        [TestCase(new[] { 1, 2, 3 }, null)]
        [TestCase(null, new[] { 4, 5, 6 })]
        [TestCase(null, null)]
        public void ConcatArraysNullCheck(int[] initialArray, int[] elementToAdd)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var result = MyArray.ConcatElementsInArray(initialArray, elementToAdd);
            });
        }

        [Test]
        [TestCase(new[] { 1, 2, 3 }, 1, new[] {2,3})]
        [TestCase(new[] { 1, 2, 3 }, 2, new[] {1,3})]
        [TestCase(new[] { 1, 2, 3 }, 3, new[] {1,2})]
        [TestCase(new[] { 1, 2, 3 }, 4, new[] {1,2,3})]
        public void RemoveElementInArray(int[] initialArray, int elementToAdd, int[] expected)
        {
            var result = MyArray.RemoveOccurenceInArray(initialArray, elementToAdd);
            Assert.That(result, Is.EquivalentTo(expected));
        }
        
        [Test]
        [TestCase(null, 1)]
        public void RemoveElementInArrayNullCheck(int[] initialArray, int elementToAdd)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                MyArray.RemoveOccurenceInArray(initialArray, elementToAdd);
            });
        }

        [Test]
        [TestCase(new[] { 1, 2, 3 }, new[] { 1,2}, new[] { 3 })]
        [TestCase(new[] { 1, 2, 3, 2, 1 }, new[] { 1 }, new[] { 2,3,2 })]
        [TestCase(new[] { 1, 2, 3, 2, 1 }, new[] { 1, 2 }, new[] { 3 })]
        [TestCase(new[] { 1, 2, 3, 2, 1 }, new[] { 1, 2, 3 }, new int[] { })]
        public void RemoveElementsInArray(int[] initialArray, int[] elementsToRemove, int[] expected)
        {
            var result = MyArray.RemoveOccurenceInArray(initialArray, elementsToRemove);
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        [TestCase(null, new[] {1,2,3})]
        [TestCase(new[] {1,2,3 }, null)]
        [TestCase(null, null)]
        public void RemoveElementsInArrayNullCheck(int[] initialArray, int[] elementToAdd)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                MyArray.RemoveOccurenceInArray(initialArray, elementToAdd);
            });
        }


    }
}