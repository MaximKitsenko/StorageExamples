using System.Threading;

namespace Powerdiary.Storage.TimeBaseStore.Engine
{
	/// <summary>
	/// Thread safe counter
	/// </summary>
	public class SimpleCounter
	{
		public int Count => _count;

		private int _count;

		public SimpleCounter()
		{
			_count = 0;
		}

		public SimpleCounter(int startCount)
		{
			_count = startCount;
		}

		public void Increment()
		{
			Interlocked.Increment(ref _count);
		}
	}
}