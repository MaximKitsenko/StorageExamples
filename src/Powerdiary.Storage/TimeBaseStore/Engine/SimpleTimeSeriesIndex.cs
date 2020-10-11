using System;
using System.Collections.Generic;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Storage.TimeBaseStore.Engine
{
	/// <summary>
	/// Thread safe in-memory time-series store
	/// </summary>
	public class SimpleTimeSeriesIndex<TKeyConverter,TValue> 
		where TKeyConverter : IDateTimeKeyConverter, new()
		where TValue : new()
	{
		private readonly SimpleIndex<DateTime, TValue> _store;
		private readonly TKeyConverter _keyConverter;

		public TValue GetOrAdd(DateTime key)
		{
			return _store.GetOrAdd(_keyConverter.GetKey(key));
		}

		public KeyValuePair<DateTime, TValue>[] GetRange(DateTime start, DateTime end)
		{
			var startKey = _keyConverter.GetKey(start);
			var endKey = _keyConverter.GetKey(end);
			var result = new List<KeyValuePair<DateTime, TValue>>();
			while (startKey <= endKey)
			{
				if (_store.TryGetValue(startKey, out var value))
				{
					result.Add(new KeyValuePair<DateTime,TValue>( startKey, value) );
				}

				startKey = _keyConverter.GetNextKey(startKey);
			};

			return result.ToArray();
		}

		public SimpleTimeSeriesIndex()
		{
			_store = new SimpleIndex<DateTime, TValue>();
			_keyConverter = new TKeyConverter();
		}
	}
}