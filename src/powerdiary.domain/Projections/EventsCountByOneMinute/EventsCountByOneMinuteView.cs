using System;
using System.Collections.Generic;
using System.Linq;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Domain.Projections.EventsCountByOneMinute
{
	public class EventsCountByOneMinuteView
	{
		public static List<KeyValuePair<DateTime, EventsCountByOneMinuteModel>> GetEventsCountAggregated(
			DateTime start,
			DateTime end, 
			IDateTimeKeyConverter customGranularity)
		{
			var aggregatedStats = InMemoryMinuteStore.Index.GetRange(start, end)
				.GroupBy(
					x => customGranularity.GetKey(x.Key),
					x => x.Value,
					(k, values) => new KeyValuePair<DateTime, EventsCountByOneMinuteModel>(
						k,
						Sum(values)));


			return aggregatedStats.ToList();
		}

		private static EventsCountByOneMinuteModel Sum(IEnumerable<EventsCountersDbValue> values)
		{
			var view = new EventsCountByOneMinuteModel(0, 0, 0, 0);

			return values.Aggregate(view, (ac, x) => ac.Add(
				x.EntersCounter.Count,
				x.ExitsCounter.Count,
				x.CommentsCounter.Count,
				x.HighFiveCounter.Count
			));
		}
	}
}