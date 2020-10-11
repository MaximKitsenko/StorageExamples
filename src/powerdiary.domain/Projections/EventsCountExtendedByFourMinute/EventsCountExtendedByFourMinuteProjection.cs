using System;
using System.Collections.Generic;
using System.Linq;
using Powerdiary.Contracts.Events;
using Powerdiary.Infrastructure;

namespace Powerdiary.Domain.Projections.EventsCountExtendedByFourMinute
{
	public class EventsCountExtendedByFourMinuteProjection :
		Handles<UserEntered>,
		Handles<UserExited>,
		Handles<FiveSent>,
		Handles<CommentSent>
	{
		public void Handle(UserEntered message)
		{
			InMemoryFourMinuteStore.Index.GetOrAdd(message.SysInfo.SentUtc).EntersCounter.Increment();
		}

		public void Handle(UserExited message)
		{
			InMemoryFourMinuteStore.Index.GetOrAdd(message.SysInfo.SentUtc).ExitsCounter.Increment();
		}

		public void Handle(FiveSent message)
		{
			var eventsCounters = InMemoryFourMinuteStore.Index.GetOrAdd(message.SysInfo.SentUtc);

			eventsCounters.HighFiveCounter.Increment();
			eventsCounters.HighFiveCounterIndex.GetOrAdd(message.SysInfo.UserId.Id)
				.Increment();
		}

		public void Handle(CommentSent comment)
		{
			InMemoryFourMinuteStore.Index.GetOrAdd(comment.SysInfo.SentUtc).CommentsCounter.Increment();
		}
	}
}