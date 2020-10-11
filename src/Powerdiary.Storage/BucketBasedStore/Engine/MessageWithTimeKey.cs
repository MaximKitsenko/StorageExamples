using System;

namespace Powerdiary.Storage.BucketBasedStore.Engine
{
	public class MessageWithTimeKey<TMessage>
	{
		public TMessage Message { get; }
		public DateTime TimeKey { get; }

		public MessageWithTimeKey(TMessage message, DateTime timeKey)
		{
			Message = message;
			TimeKey = timeKey;
		}
	}
}