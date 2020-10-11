using System;
using Powerdiary.Contracts.Events;
using Powerdiary.Storage.BucketBasedStore.Engine;
using Powerdiary.Storage.TimeBaseStore.Engine;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Domain.Projections.EventsWholeHistory
{
	public static class InMemoryMessageStore
	{
		public static BucketsBasedIndex<JustUglyMessage> Index =
			new BucketsBasedIndex<JustUglyMessage>(new ConcurrentBucketFactory<JustUglyMessage>());
	}

	/// <summary>
	/// For production usage message should be optimized
	/// </summary>
	public class JustUglyMessage
	{
		public FiveSent FiveSent { get; }
		public UserExited UserExited { get; }
		public CommentSent CommentSent { get; }
		public UserEntered UserEntered { get; }

		public JustUglyMessage(CommentSent commentSent)
		{
			CommentSent = commentSent;
		}
		public JustUglyMessage(UserEntered userEntered)
		{
			UserEntered = userEntered;
		}

		public JustUglyMessage(FiveSent fiveSent)
		{
			FiveSent = fiveSent;
		}

		public JustUglyMessage(UserExited userExited)
		{
			UserExited = userExited;
		}
	}
}