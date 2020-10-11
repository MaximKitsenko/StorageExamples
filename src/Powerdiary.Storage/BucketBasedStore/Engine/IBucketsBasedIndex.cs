using System;
using System.Collections.Generic;

namespace Powerdiary.Storage.BucketBasedStore.Engine
{
	public interface IBucketsBasedIndex<T>
	{
		void Add(DateTime key, T msg);
		void Remove(DateTime key);
		IEnumerable<T> Get(DateTime dateTimeStart, DateTime dateTimeEnd);
		IEnumerable<MessageWithTimeKey<T>> GetMessagesWithTimeKey(DateTime dateTimeStart, DateTime dateTimeEnd);
	}
}