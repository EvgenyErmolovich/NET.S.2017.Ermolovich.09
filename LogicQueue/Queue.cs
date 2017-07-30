using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LogicQueue
{
	public class Queue<T> : IEnumerable<T>, IEnumerable, IEquatable<Queue<T>>, ICollection, IReadOnlyCollection<T> where T : IEquatable<T>
	{
		/// <summary>
		/// The array.
		/// </summary>
		private T[] array;
		/// <summary>
		/// The count.
		/// </summary>
		private int count;
		/// <summary>
		/// The capacity.
		/// </summary>
		private int capacity = 8;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LogicQueue.Queue`1"/> class.
		/// </summary>
		public Queue()
		{
			array = new T[capacity];
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
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="T:LogicQueue.Queue`1"/> class.
		/// </summary>
		/// <param name="collection">Collection.</param>
		public Queue(IEnumerable<T> collection): this()
		{
			if (ReferenceEquals(collection, null)) throw new ArgumentNullException($"{nameof(collection)} is null.");

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

			if (isFull) Expansion();

			array[count] = element;
			count++;
		}
		/// <summary>
		/// Dequeue this instance.
		/// </summary>
		public void Dequeue()
		{
			if (IsEmpty) throw new ArgumentException("Queue is empty.");

			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = array[i + 1];
			}
			array[--count] = default(T);
		}
		/// <summary>
		/// Dequeue the specified quantity.
		/// </summary>
		/// <returns>The dequeue.</returns>
		/// <param name="quantity">Quantity.</param>
		public void Dequeue(int quantity)
		{
			if (quantity < 0 || quantity > array.Length) throw new ArgumentNullException($"{nameof(quantity)} is unsuitable.");

			while (quantity > 0)
			{
				Dequeue();
				quantity--;
			}
		}
		/// <summary>
		/// Peek this instance.
		/// </summary>
		/// <returns>The peek.</returns>
		public T Peek()
		{
			if (IsEmpty) throw new ArgumentException("Queue is empty.");

			return array[0];
		}
		/// <summary>
		/// Contains the specified element.
		/// </summary>
		/// <returns>The contains.</returns>
		/// <param name="element">Element.</param>
		public bool Contains(T element)
		{
			if (IsEmpty) throw new ArgumentException("Queue is empty.");
			if (ReferenceEquals(element, null)) throw new ArgumentNullException($"{nameof(element)} is null.");

			for (int i = 0; i < count; i++)
			{
				if (element.Equals(array[i]))
					return true;
			}
			return false;
		}
		/// <summary>
		/// Clear this instance.
		/// </summary>
		public void Clear()
		{
			if (IsEmpty) throw new ArgumentException("Queue is empty.");

			count = 0;
			capacity = 10;
			array = new T[capacity];
		}
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:LogicQueue.Queue`1"/> is full.
		/// </summary>
		/// <value><c>true</c> if is full; otherwise, <c>false</c>.</value>
		private bool isFull => count == capacity;
		/// <summary>
		/// Gets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count => count;
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:LogicQueue.Queue`1"/> is empty.
		/// </summary>
		/// <value><c>true</c> if is empty; otherwise, <c>false</c>.</value>
		public bool IsEmpty => count == 0;
		/// <summary>
		/// Gets a value indicating whether this <see cref="T:LogicQueue.Queue`1"/> is synchronized.
		/// </summary>
		/// <value><c>true</c> if is synchronized; otherwise, <c>false</c>.</value>
		public bool IsSynchronized
		{
			get
			{
				return array.IsSynchronized;
			}
		}
		/// <summary>
		/// Gets the sync root.
		/// </summary>
		/// <value>The sync root.</value>
		public object SyncRoot
		{
			get
			{
				return array.SyncRoot;
			}
		}
		/// <summary>
		/// Copies to.
		/// </summary>
		/// <param name="array">Array.</param>
		/// <param name="index">Index.</param>
		public void CopyTo(Array array, int index)
		{
			this.array.CopyTo(array, index);
		}
		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:LogicQueue.Queue`1"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:LogicQueue.Queue`1"/>.</returns>
		public override string ToString()
		{
			if (IsEmpty) return "Queue is empty.";

			StringBuilder str = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				str.Append(array[i].ToString() + " ");
			}
			return str.ToString();
		}
		/// <summary>
		/// Determines whether the specified <see cref="LogicQueue.Queue<T>"/> is equal to the current <see cref="T:LogicQueue.Queue`1"/>.
		/// </summary>
		/// <param name="other">The <see cref="LogicQueue.Queue<T>"/> to compare with the current <see cref="T:LogicQueue.Queue`1"/>.</param>
		/// <returns><c>true</c> if the specified <see cref="LogicQueue.Queue<T>"/> is equal to the current
		/// <see cref="T:LogicQueue.Queue`1"/>; otherwise, <c>false</c>.</returns>
		public bool Equals(Queue<T> other)
		{
			if (ReferenceEquals(other, null)) throw new ArgumentNullException($"{nameof(other)} is null.");

			if (other.Count != this.Count) return false;

			for (int i = 0; i < this.Count; i++)
			{
				if (!this.array[i].Equals(other.array[i]))
					return false;
			}
			return true;
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
					return queue.array[index];
				}
			}

			object IEnumerator.Current => Current;

			public IteratorQueue(Queue<T> queue)
			{
				this.queue = queue;
				this.index = -1;
			}

			public bool MoveNext() => ++index < queue.Count;

			public void Reset() => index = -1;

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
	}
}