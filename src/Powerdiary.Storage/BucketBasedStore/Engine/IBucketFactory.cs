namespace Powerdiary.Storage.BucketBasedStore.Engine
{
	public interface IBucketFactory<TMessage>
	{
		IBucket<TMessage> CreateBucket(long bucketKey);
	}
}