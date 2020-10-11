namespace Powerdiary.Storage.BucketBasedStore.Engine
{
	public class ConcurrentBucketFactory<TMessage> : IBucketFactory<TMessage>
	{
		public IBucket<TMessage> CreateBucket(long bucketKey)
		{
			return new ConcurrentBucket<TMessage>(bucketKey);
		}
	}
}