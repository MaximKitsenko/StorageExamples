using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Powerdiary.Storage.BucketBasedStore.Engine
{
	public static class Extensions
	{
		private const long TicksBetweenDays = 864000000000;

		public static TV GetValueOrDefault<TK, TV>(this ConcurrentDictionary<TK, TV> src, TK key)
		{
			return src.TryGetValue(key, out var value) ? value : default(TV);
		}

		public static long ToDayBucketKey(this DateTime dateTime)
		{
			var dateTimeUtc = dateTime.ToUniversalTime();
			return new DateTime(dateTimeUtc.Year, dateTimeUtc.Month, dateTimeUtc.Day, 0, 0, 0, DateTimeKind.Utc).Ticks;
		}

		public static KeyValuePair<TKey, TValue> CreateKeyValuePair<TKey, TValue>(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key,value);
		}

		public static IEnumerable<long> GetBucketKeysRange(this DateTime dateTimeStart, DateTime dateTimeEnd)
		{
			//if (dateTimeStart > dateTimeEnd)
			//{
			//	yield return new List<long>();
			//}

			var dt1 = dateTimeStart.ToUniversalTime().ToDayBucketKey();
			var dt2 = dateTimeEnd.ToUniversalTime().ToDayBucketKey();

			var keysCount = ((dt2 - dt1) / TicksBetweenDays)+1;

			for (long i = 0; i < keysCount; i++)
			{
				yield return dt1+  i * TicksBetweenDays;
			}
		}

		public static long ToNextBucketKey(this long key)
		{
			return key + TicksBetweenDays;
		}

		public static List<T> AddReturn<T>(this List<T> list, T val)
		{
			list.Add(val);
			return list;
		}
	}
}