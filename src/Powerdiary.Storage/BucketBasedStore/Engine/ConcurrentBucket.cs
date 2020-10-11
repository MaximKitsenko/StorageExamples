using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Powerdiary.Storage.BucketBasedStore.Engine
{
	/// <summary>
	/// Thread-safe bucket
	/// </summary>
	/// <typeparam name="TMessage"></typeparam>
	public class ConcurrentBucket<TMessage> : IBucket<TMessage>
	{
		private long BucketId { get; } // we need it to filter messages inside bucket
		private ConcurrentDictionary<DateTime, List<TMessage>> Index { get; }

		public ConcurrentBucket(long bucketId)
		{
			this.BucketId = bucketId;
			this.Index = new ConcurrentDictionary<DateTime, List<TMessage>>();
		}

		public void Add(DateTime key, TMessage msg)
		{
			Index.AddOrUpdate(key, new List<TMessage>() {msg}, (k, v) => v.AddReturn(msg));
		}

		public bool TryRemove(DateTime key)
		{
			return Index.TryRemove(key, out var collisionBucket);
		}

		public IEnumerable<TMessage> GetMessages()
		{
			return Index.SelectMany(x => x.Value);
		}

		public IEnumerable<TMessage> GetMessages(DateTime startDate, DateTime endDate)
		{
			var firstBucket = startDate.ToBucketKey();
			var lastBucketKey = endDate.ToBucketKey();
			var result = Enumerable.Empty<TMessage>();

			if (this.BucketId >= firstBucket && this.BucketId <= lastBucketKey || startDate >= endDate)
			{
				result = (this.GetFilterFunc(startDate, endDate, out var filter) ? Index.Where(filter) : Index)
					.SelectMany(x => x.Value);
			}

			return result;
		}

		public IEnumerable<MessageWithTimeKey<TMessage>> GetMessagesWithTime(DateTime startDate, DateTime endDate)
		{
			var firstBucket = startDate.ToBucketKey();
			var lastBucketKey = endDate.ToBucketKey();
			var result = Enumerable.Empty<MessageWithTimeKey<TMessage>>();

			if (this.BucketId >= firstBucket && this.BucketId <= lastBucketKey || startDate >= endDate)
			{
				result = (this.GetFilterFunc(startDate, endDate, out var filter) ? Index.Where(filter) : Index)
					.SelectMany(x => x.Value.Select(y=> new MessageWithTimeKey<TMessage>(y,x.Key)) );
			}

			return result;
		}

		private bool GetFilterFunc(
			DateTime startDate,
			DateTime endDate,
			out Func<KeyValuePair<DateTime, List<TMessage>>, bool> filter
		)
		{
			var firstBucket = startDate.ToBucketKey();
			var lastBucketKey = endDate.ToBucketKey();

			if (this.BucketId > firstBucket) // no need to filter by startDate
			{
				if (this.BucketId < lastBucketKey) // no need to filter by endDate
				{
					filter = null;
				}
				else
				{
					filter = x => x.Key <= endDate;
				}
			}
			else // need to filter by startDate
			{
				if (this.BucketId < lastBucketKey) // no need to filter by endDate
				{
					filter = x => x.Key >= startDate;
				}
				else // start date = end date = bucket
				{
					filter = x => x.Key >= startDate && x.Key <= endDate;
				}
			}

			return filter != null;
		}
	}
}