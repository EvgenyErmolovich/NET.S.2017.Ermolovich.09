using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LogicQueue
{
	public class Queue<T> : IEnumerable<T>
	{
		/// <summary>
		/// The array.
		/// </summary>
		private T[] array;
		/// <summary>
		/// The head.
		/// </summary>
		private int head;
		/// <summary>
		/// The tail.
		/// </summary>
		private int tail;
		/// <summary>
		/// The capacity.
		/// </summary>
		private int capacity = 8;
		/// <summary>
		/// The count.
		/// </summary>
		private int count;
		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count => count;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LogicQueue.Queue`1"/> class.
		/// </summary>
		public Queue()
		{
			array = new T[capacity];
			head = -1;
            tail = -1;
            count = 0;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LogicQueue.Queue`1"/> class.
		/// </summary>
		/// <param name="capacity">Capacity.</param>
		public Queue(int capacity)
		{
			if (capacity <= 0) throw new ArgumentException($"{nameof(capacity)} is unsuitable.");
			this.capacity = capacity;
			array = new T[capacity];
			head = -1;
            tail = -1;
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LogicQueue.Queue`1"/> class.
		/// </summary>
		/// <param name="collection">Collection.</param>
		public Queue(IEnumerable<T> collection): this()
		{
			if (ReferenceEquals(collection, null)) throw new ArgumentNullException($"{nameof(collection)} is null.");
			head = -1;
            tail = -1;
			foreach (T element in collection)
			{
				Enqueue(element);
			}
		}
		/// <summary>
		/// Enqueue the specified element.
		/// </summary>
		/// <returns>The enqueue.</returns>
		/// <param name="element">Element.</param>
		public void Enqueue(T element)
		{
			if (ReferenceEquals(element, null)) throw new ArgumentNullException($"{nameof(element)} is null.");

			if (isFull()) Expansion();

			array[++tail % capacity] = element;
            count++;
		}
		/// <summary>
		/// Dequeue this instance.
		/// </summary>
		public T Dequeue()
		{
			if (IsEmpty()) throw new ArgumentException("Queue is empty.");

			count--;
			return array[++head % capacity];
		}
		/// <summary>
		/// Peek this instance.
		/// </summary>
		/// <returns>The peek.</returns>
		public T Peek
		{
			get
			{
				if (IsEmpty()) throw new ArgumentException("Queue is empty.");
				return array[(head + 1) % capacity];
			}
		}
		/// <summary>
		/// Gets the enumerator.
		/// </summary>
		/// <returns>The enumerator.</returns>
		public IEnumerator<T> GetEnumerator() => new IteratorQueue(this);

		IEnumerator IEnumerable.GetEnumerator() => new IteratorQueue(this);
		/// <summary>
		/// Iterator queue.
		/// </summary>
		private struct IteratorQueue : IEnumerator<T>
		{
			private Queue<T> queue;
			private int index;

			public T Current
			{
				get
				{
					if (index == -1 || index == queue.Count) throw new ArgumentException($"{nameof(index)} is unsuitable.");
					return queue.array[index % queue.capacity];
				}
			}

			object IEnumerator.Current
			{
                get
                {
					return queue.array[index % queue.capacity];
                }
            }

			public IteratorQueue(Queue<T> queue)
			{
				this.queue = queue;
				index = queue.head;
			}

			public bool MoveNext()
			{

                while (index < queue.tail)
                {
                    index++;
                    return true;
                }
                return false;
            }

			public void Reset() => index = queue.head;

			public void Dispose() { }
		}
		/// <summary>
		/// Expansion this instance.
		/// </summary>
		private void Expansion()
		{
			capacity *= 2;
			Array.Resize(ref array, capacity);
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:LogicQueue.Queue`1"/> is full.
		/// </summary>
		/// <value><c>true</c> if is full; otherwise, <c>false</c>.</value>
		private bool isFull() => count == capacity;
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:LogicQueue.Queue`1"/> is empty.
		/// </summary>
		/// <value><c>true</c> if is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty() => count == 0;
	}
}