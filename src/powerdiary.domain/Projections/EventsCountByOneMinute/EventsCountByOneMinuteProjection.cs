using System;
using System.Collections.Generic;
using System.Linq;
using Powerdiary.Contracts.Events;
using Powerdiary.Infrastructure;

namespace Powerdiary.Domain.Projections.EventsCountByOneMinute
{
	public class EventsCountByOneMinuteProjection :
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
			InMemoryMinuteStore
				.Index
				.GetOrAdd(message.SysInfo.SentUtc)
				.HighFiveCounter
				.Increment();
		}

		public void Handle(CommentSent comment)
		{
			InMemoryMinuteStore
				.Index
				.GetOrAdd(comment.SysInfo.SentUtc)
				.CommentsCounter
				.Increment();
		}
	}
}