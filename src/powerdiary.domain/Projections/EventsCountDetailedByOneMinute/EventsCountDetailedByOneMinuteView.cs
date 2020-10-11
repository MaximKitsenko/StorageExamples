using System;
using System.Collections.Generic;
using System.Linq;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Domain.Projections.EventsCountDetailedByOneMinute
{
	public static class EventsCountDetailedByOneMinuteView
	{
		public static List<KeyValuePair<DateTime, EventsCountDetailedByOneMinuteViewModel>> GetEventsCount(
			DateTime start,
			DateTime end)
		{
			return InMemoryMinuteStore
				.Index
				.GetRange(start, end)
				.Select(x => new KeyValuePair<DateTime, EventsCountDetailedByOneMinuteViewModel>(x.Key, new EventsCountDetailedByOneMinuteViewModel(
					x.Value.EntersCounter.Count,
					x.Value.ExitsCounter.Count,
					x.Value.CommentsCounter.Count,
					x.Value.HighFiveCounter.Count)))
				.ToList();
		}

		public static List<KeyValuePair<DateTime, EventsCountDetailedByOneMinuteViewModel>> GetEventsCountAggregated(
			DateTime start,
			DateTime end,
			IDateTimeKeyConverter customGranularity)
		{
			var aggregatedStats = InMemoryMinuteStore.Index.GetRange(start, end)
				.GroupBy(
					x => customGranularity.GetKey(x.Key),
					x => x.Value,
					(k, values) => new KeyValuePair<DateTime, EventsCountDetailedByOneMinuteViewModel>(
						k,
						AggregateDbValuesToEventsCountDetailedViewModel(values)));


			return aggregatedStats.ToList();
		}

		private static EventsCountDetailedByOneMinuteViewModel AggregateDbValuesToEventsCountDetailedViewModel(IEnumerable<EventsCountersDbValue> eventsCountersDbValues)
		{
			var countersDbValues = eventsCountersDbValues.ToList();

			var fivesByUserIndex = countersDbValues
				.SelectMany(x => x.HighFiveCounterDoubleIndex.GetKeysAndValues())
				.GroupBy(x => x.Key)
				.Select(y => new KeyValuePair<Guid, Dictionary<Guid, int>>(
					y.Key,
					y
						.SelectMany(z => z.Value.GetKeysAndValues())
						.GroupBy(i => i.Key)
						.Select(j => new KeyValuePair<Guid, int>(j.Key, j.Sum(k => k.Value.Count)))
						.ToDictionary(a => a.Key, b => b.Value)
				)).ToDictionary(c => c.Key, d => d.Value);

			var enters = 0;
			var exits = 0;
			var comments = 0;
			var fives = 0;

			foreach (var countersDto in countersDbValues)
			{
				enters += countersDto.EntersCounter.Count;
				exits += countersDto.ExitsCounter.Count;
				comments += countersDto.CommentsCounter.Count;
				fives += countersDto.HighFiveCounter.Count;
			}

			var view = new EventsCountDetailedByOneMinuteViewModel(enters,exits,comments,fives, fivesByUserIndex);

			return view;
		}
	}
}