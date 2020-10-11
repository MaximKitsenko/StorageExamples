using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Powerdiary.Storage.TimeBaseStore.Engine
{
	/// <summary>
	/// Thread safe key-value index
	/// </summary>
	public class SimpleIndex<TKey, TValue> where TValue : new()
	{
		private readonly ConcurrentDictionary<TKey, TValue> _index;

		public SimpleIndex()
		{
			_index = new ConcurrentDictionary<TKey, TValue>();
		}

		public TValue GetOrAdd(TKey key)
		{
			var value = _index.GetOrAdd(key, new TValue());
			return value;
		}

		public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addFunc, Func<TKey, TValue, TValue> updateFunc)
		{
			var value = _index.AddOrUpdate(key, addFunc, updateFunc);
			return value;
		}

		public ICollection<TKey> GetKeys()
		{
			var value = _index.Keys;
			return value;
		}

		public List<KeyValuePair<TKey, TValue>> GetKeysAndValues()
		{
			var value = _index.ToList();
			return value;
		}

		public bool TryGetValue(TKey key, out TValue val)
		{
			return _index.TryGetValue(key, out val);
		}
	}
}