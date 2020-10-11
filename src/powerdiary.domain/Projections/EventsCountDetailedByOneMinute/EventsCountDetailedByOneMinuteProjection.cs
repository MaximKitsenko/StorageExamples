using Powerdiary.Contracts.Events;
using Powerdiary.Infrastructure;

namespace Powerdiary.Domain.Projections.EventsCountDetailedByOneMinute
{
	public class EventsCountDetailedByOneMinuteProjection :
		Handles<UserEntered>,
		Handles<UserExited>,
		Handles<FiveSent>,
		Handles<CommentSent>
	{

		public void Handle(UserEntered message)
		{
			InMemoryMinuteStore
				.Index
				.GetOrAdd(message.SysInfo.SentUtc)
				.EntersCounter
				.Increment();
		}

		public void Handle(UserExited message)
		{
			InMemoryMinuteStore
				.Index
				.GetOrAdd(message.SysInfo.SentUtc)
				.ExitsCounter
				.Increment();
		}

		public void Handle(FiveSent message)
		{
			var eventsCounters = InMemoryMinuteStore.Index.GetOrAdd(message.SysInfo.SentUtc);
			eventsCounters
				.HighFiveCounter
				.Increment();
			eventsCounters
				.HighFiveCounterDoubleIndex
				.GetOrAdd(message.SysInfo.UserId.Id)
				.GetOrAdd(message.ToUserId.Id)
				.Increment();
		}

		public void Handle(CommentSent comment)
		{
			InMemoryMinuteStore.Index.GetOrAdd(comment.SysInfo.SentUtc).CommentsCounter.Increment();
		}
	}
}