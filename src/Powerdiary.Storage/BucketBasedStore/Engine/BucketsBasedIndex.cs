using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Powerdiary.Storage.BucketBasedStore.Engine
{
	public class BucketsBasedIndex<TMessage> : IBucketsBasedIndex<TMessage>
	{
		private readonly IBucketFactory<TMessage> _bucketFactory;
		private readonly ConcurrentDictionary<long, IBucket<TMessage>> _buckets = new ConcurrentDictionary<long, IBucket<TMessage>>();

		public BucketsBasedIndex(IBucketFactory<TMessage> bucketFactory)
		{
			_bucketFactory = bucketFactory;
		}

		public void Add(DateTime key, TMessage msg)
		{
			var bucketKey = key.ToDayBucketKey();
			var bucket = _buckets.GetOrAdd(bucketKey, _bucketFactory.CreateBucket(bucketKey) );
			bucket.Add(key, msg);
		}

		public void Remove(DateTime key)
		{
			if (_buckets.TryGetValue(key.ToDayBucketKey(), out var bucket))
			{
				bucket.TryRemove(key);
			}
		}

		public IEnumerable<TMessage> Get(DateTime dateTimeStart, DateTime dateTimeEnd)
		{
			return dateTimeStart
				.GetBucketKeysRange(dateTimeEnd)
				.Select(x => this._buckets.GetValueOrDefault(x))
				.Where(x => x != null)
				.SelectMany(x => x.GetMessages(dateTimeStart, dateTimeEnd));
		}

		public IEnumerable<MessageWithTimeKey<TMessage>> GetMessagesWithTimeKey(DateTime dateTimeStart, DateTime dateTimeEnd)
		{
			return dateTimeStart
				.GetBucketKeysRange(dateTimeEnd)
				.Select(x => this._buckets.GetValueOrDefault(x))
				.Where(x => x != null)
				.SelectMany(x => x.GetMessagesWithTime(dateTimeStart, dateTimeEnd));
		}
	}
}