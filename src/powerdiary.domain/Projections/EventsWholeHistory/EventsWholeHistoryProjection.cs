using System;
using System.Collections.Generic;
using System.Linq;
using Powerdiary.Contracts.Events;
using Powerdiary.Infrastructure;

namespace Powerdiary.Domain.Projections.EventsWholeHistory
{
	public class EventsWholeHistoryProjection :
		Handles<UserEntered>,
		Handles<UserExited>,
		Handles<FiveSent>,
		Handles<CommentSent>
	{
		public void Handle(UserEntered message)
		{
			InMemoryMessageStore
				.Index
				.Add(message.SysInfo.SentUtc, new JustUglyMessage(message));
		}

		public void Handle(UserExited message)
		{
			InMemoryMessageStore
				.Index
				.Add(message.SysInfo.SentUtc, new JustUglyMessage(message));
		}

		public void Handle(FiveSent message)
		{
			InMemoryMessageStore
				.Index
				.Add(message.SysInfo.SentUtc, new JustUglyMessage(message));
		}

		public void Handle(CommentSent message)
		{
			InMemoryMessageStore
				.Index
				.Add(message.SysInfo.SentUtc, new JustUglyMessage(message));
		}
	}
}