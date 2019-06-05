using System.Collections;
using System.Collections.Generic;

namespace AlgorithmicTraining.Lib
{
    /// <summary>
    /// Singly Linked List Collection - reduced functionality for demo
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CustomLinkedList<T> : IEnumerable<T>
    {
        #region internal custom linked list logic

        /// <summary>
        /// Singly Linked List Node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class CustomLinkedListNode<T>
        {
            public T Data { get; set; }
            public CustomLinkedListNode<T> Next { get; set; }

            public CustomLinkedListNode(T data)
            {
                Data = data;
                Next = null;
            }

            public override string ToString()
            {
                return Data.ToString();
            }
        }

        public CustomLinkedListNode<T> Head { get; private set; } = null;

        public CustomLinkedListNode<T> Tail { get; private set; } = null;

        public int Count { get; private set; } = 0;

        public CustomLinkedList(IEnumerable<T> initCollection)
        {
            foreach (var item in initCollection)
            {
                Add(item);
            }
        }

        public void Add(T data)
        {
            var item = new CustomLinkedListNode<T>(data);
            if (Head == null)
            {
                Head = item;
            }
            else
            {
                Tail.Next = item;
            }

            Tail = item;
            Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        /// <summary>
        /// reverse linked list
        /// </summary>
        public void Reverse()
        {

            if (Head == null) return;

            CustomLinkedListNode<T> previous = null, current = Head, next = null;

            while (current.Next != null)
            {
                next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }

            current.Next = previous;
            Head = current;

        }
    }
}