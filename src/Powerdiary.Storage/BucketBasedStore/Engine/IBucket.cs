using System;
using System.Collections.Generic;

namespace Powerdiary.Storage.BucketBasedStore.Engine
{
	public interface IBucket<TM>
	{
		void Add(DateTime key, TM msg);
		bool TryRemove(DateTime key);
		IEnumerable<TM> GetMessages();
		IEnumerable<TM> GetMessages(DateTime startDate, DateTime endDate);
		IEnumerable<MessageWithTimeKey<TM>> GetMessagesWithTime(DateTime startDate, DateTime endDate);
	}
}