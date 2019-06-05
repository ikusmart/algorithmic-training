using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmicTraining.Lib;
using Shouldly;
using Xunit;

namespace AlgorithmicTraining.Tests
{
    public class FindTwoMissingItemsTests
    {
        private static (int firstItem, int secondItem, List<int> initCollection) GenerateTestData(int n)
        {
            var capacity = n > 0 ? n-1 : 0;
            var resultCollection = new List<int>(capacity);
            var hiddenItems = GenerateTwoHiddenItems(n);

            for (int i = 0; i <= n; i++)
            {
                if (i != hiddenItems.firstItem && i != hiddenItems.secondItem)
                {
                    resultCollection.Add(i);
                }
            }

            return (hiddenItems.firstItem, hiddenItems.secondItem, resultCollection);
        }

        private static (int firstItem, int secondItem) GenerateTwoHiddenItems(int n)
        {
            if (n < 1) return (0, 0);
            if (n == 1) return (0, 1);

            var random = new Random(Environment.TickCount);
            var firstItem = random.Next(0, n);
            int secondItem = 0;

            do { secondItem = random.Next(0, n); } while (firstItem == secondItem);
            return (firstItem, secondItem);
        }

        

        public static IEnumerable<object[]> FindTwoItemsManualData =>
            new[]
            {
                new object[] { new int[] { 1,2,3,4,6,7 }, 0, 5 },
                new object[] { new int[] { 1 }, 0, 2 },
                new object[] { new int[] { 10, 9, 8 , 5, 4, 3, 2, 1, 0 }, 7, 6 },
            };

        [Theory, MemberData(nameof(FindTwoItemsManualData))]
        public void FindTwoItems_ValidManualData(int[] initialArray, int firstExpectedItem, int secondExpectedItem)
        {
            var result = FindTwoLostItemsInArrayTask.FindTwoMissingItems(initialArray);

            result.HasValue.ShouldBe(true);

            Math.Min(result.Value.firstLostItem, result.Value.secondLostItem).ShouldBe(Math.Min(firstExpectedItem, secondExpectedItem));
            Math.Max(result.Value.firstLostItem, result.Value.secondLostItem).ShouldBe(Math.Max(firstExpectedItem, secondExpectedItem));
        }


        public static IEnumerable<object[]> FindTwoItemsRandomData =>
            new[]
            {
                new object[] { 2 },
                new object[] { 3 },
                new object[] { 100 },
                new object[] { 1000 },
                new object[] { 10000 },
                new object[] { 100000 },
                new object[] { 1000000 },
            };
        [Theory, MemberData(nameof(FindTwoItemsRandomData))]
        public void FindTwoItems_ValidRandomData(int n)
        {
            var testData = GenerateTestData(n);

            var firstExpectedItem = testData.firstItem;
            var secondExpectedItem = testData.secondItem;
            var initArray = testData.initCollection.ToArray();

            var result = FindTwoLostItemsInArrayTask.FindTwoMissingItems(initArray);

            result.HasValue.ShouldBe(true);

            Math.Min(result.Value.firstLostItem, result.Value.secondLostItem).ShouldBe(Math.Min(firstExpectedItem, secondExpectedItem));
            Math.Max(result.Value.firstLostItem, result.Value.secondLostItem).ShouldBe(Math.Max(firstExpectedItem, secondExpectedItem));
        }

        public static IEnumerable<object[]> NotValidData =>
            new[]
            {
                new object[] { 0 },
                new object[] { 1 },
            };

        [Theory, MemberData(nameof(NotValidData))]
        public void NotValidDataTest(int n)
        {
            var testData = GenerateTestData(n);
            var initArray = testData.initCollection.ToArray();
            var result = FindTwoLostItemsInArrayTask.FindTwoMissingItems(initArray);

            result.HasValue.ShouldBe(false);

        }

    }
}