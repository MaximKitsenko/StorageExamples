using System;
using System.Collections.Generic;
using System.Linq;
using Powerdiary.Storage.BucketBasedStore.Engine;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Domain.Projections.EventsWholeHistory
{
	public class EventsWholeHistoryView
	{
		public static List<KeyValuePair<DateTime, EventsWholeHistoryModel>> GetEventsCountAggregated(
			DateTime start,
			DateTime end,
			IDateTimeKeyConverter customGranularity)
		{
			var aggregatedStats = InMemoryMessageStore
				.Index
				.GetMessagesWithTimeKey(start, end)
				.GroupBy(
					x => customGranularity.GetKey(x.TimeKey),
					(newTimeKey, msgs) => CreateModel(msgs, newTimeKey))
				.OrderBy(x=>x.Key);

			return aggregatedStats.ToList();
		}

		private static KeyValuePair<DateTime, EventsWholeHistoryModel> CreateModel(IEnumerable<MessageWithTimeKey<JustUglyMessage>> msgs, DateTime newTimeKey)
		{
			var enters = 0;
			var exits = 0;
			var fives = 0;
			var comments = 0;
			var fivesToOther = new Dictionary<Guid, int>();
			foreach (var msg in msgs)
			{
				(enters, exits, fives, comments) = Enters(msg, enters, exits, fives, comments, fivesToOther);
			}

			return new KeyValuePair<DateTime, EventsWholeHistoryModel>(newTimeKey, new EventsWholeHistoryModel(enters, exits, comments, fives, fivesToOther));
		}

		private static (int enters, int exits, int fives, int comments) Enters(
			MessageWithTimeKey<JustUglyMessage> msg,
			int enters,
			int exits, int fives, int comments,
			Dictionary<Guid, int> fivesToOther)
		{
			if (msg.Message.UserEntered != null)
			{
				return (++enters, exits, fives, comments);
			}
			else if (msg.Message.UserExited != null)
			{
				return (enters, ++exits, fives, comments);
			}
			else if (msg.Message.CommentSent != null)
			{
				return (enters, exits, fives, ++comments);
			}
			else if (msg.Message.FiveSent != null)
			{
				var userId = msg.Message.FiveSent.SysInfo.UserId.Id;

				fivesToOther.TryGetValue(userId, out var value);
				fivesToOther[userId] = value + 1;

				return (enters, exits, ++fives, comments);
			}

			return (enters, exits, fives, comments);
		}
	}
}