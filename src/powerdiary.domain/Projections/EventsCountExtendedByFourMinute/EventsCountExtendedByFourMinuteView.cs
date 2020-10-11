using System;
using System.Collections.Generic;
using System.Linq;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Domain.Projections.EventsCountExtendedByFourMinute
{
	public static class EventsCountExtendedByFourMinuteView
	{
		public static List<KeyValuePair<DateTime, EventsCountExtendedByFourMinuteModel>> GetEventsCountAggregated(
			DateTime start,
			DateTime end,
			IDateTimeKeyConverter customGranularity)
		{
			var aggregatedStats = InMemoryFourMinuteStore.Index.GetRange(start, end)
				.GroupBy(
					x => customGranularity.GetKey(x.Key),
					x => x.Value,
					(k, values) => new KeyValuePair<DateTime, EventsCountExtendedByFourMinuteModel>(
						k,
						AggregateDataToEventsCountersView(values)));


			return aggregatedStats.ToList();
		}

		private static EventsCountExtendedByFourMinuteModel AggregateDataToEventsCountersView(IEnumerable<EventsCountersDbValue> eventsCountersDbValues)
		{
			var countersDbValues = eventsCountersDbValues.ToList();

			var fivesByUserIndex = countersDbValues
				.SelectMany(x => x.HighFiveCounterIndex.GetKeysAndValues())
				.GroupBy(x => x.Key)
				.Select(y => new KeyValuePair<Guid, int>(
					y.Key,
					y.Sum(z=>z.Value.Count)
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

			var view = new EventsCountExtendedByFourMinuteModel(enters, exits, comments, fives, fivesByUserIndex);

			return view;
		}
	}
}