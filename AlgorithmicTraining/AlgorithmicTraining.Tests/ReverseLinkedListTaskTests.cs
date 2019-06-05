using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AlgorithmicTraining.Lib;
using Shouldly;
using Xunit;

namespace AlgorithmicTraining.Tests
{
    public class ReverseLinkedListTaskTests
    {
        private static IEnumerable<int> GenerateIntArray(int n)
        {
            n = n >= 0 ? n : 100;

            Random _rand = new Random();
            var results = Enumerable.Range(0, n)
                .Select(r => _rand.Next(99999))
                .ToList();

            return results;
        }

        private static IEnumerable<string> GenerateStringArray(int n)
        {
            return GenerateIntArray(n).Select(x => x.ToString());
        }


        public static IEnumerable<object[]> ReverseIntLinkedList =>
            new[]
            {
                new object[] { new int[]{} },
                new object[] { new int[]{ 1,2,4,5,1,5,6,7,2,1,6 } },
                new object[] { new int[] { -7, 1, 4, 23333, 3} },
                new object[] { GenerateIntArray(10) },
                new object[] { GenerateIntArray(999) },
                new object[] { GenerateIntArray(500) },
            };


        [Theory, MemberData(nameof(ReverseIntLinkedList))]
        public void TestIntLinkedList(int[] initArray)
        {
            var linkedList = new CustomLinkedList<int>(initArray);

            linkedList.Reverse();

            linkedList.Count.ShouldBe(initArray.Length);

            var currentItem = linkedList.Head;
            for (int i = initArray.Length - 1; i >= 0; i--)
            {
                initArray[i].ShouldBe(currentItem.Data);
                currentItem = currentItem.Next;
            }
        }


        public static IEnumerable<object[]> ReverseStringLinkedList =>
            new[]
            {
                new object[] { new string[]{} },
                new object[] { new string[]{ default, "", default } },
                new object[] { new string[]{ "123", "456", "qwe", "aasdasd", "asdasd", "22312" } },
                new object[] { GenerateStringArray(10) },
                new object[] { GenerateStringArray(999) },
                new object[] { GenerateStringArray(500) },
            };

        [Theory, MemberData(nameof(ReverseStringLinkedList))]
        public void TestStringLinkedList(string[] initArray)
        {
            var linkedList = new CustomLinkedList<string>(initArray);

            linkedList.Reverse();

            linkedList.Count.ShouldBe(initArray.Length);

            var currentItem = linkedList.Head;
            for (int i = initArray.Length - 1; i >= 0; i--)
            {
                initArray[i].ShouldBe(currentItem.Data);
                currentItem = currentItem.Next;
            }
        }
    }
}
