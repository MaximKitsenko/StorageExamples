using System;
using Powerdiary.Storage.TimeBaseStore.Engine;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Domain.Projections.EventsCountByOneMinute
{
	public static class InMemoryMinuteStore
	{
		public static SimpleTimeSeriesIndex<MinuteKey, EventsCountersDbValue> Index =
			new SimpleTimeSeriesIndex<MinuteKey, EventsCountersDbValue>();
	}

	/// <summary>
	/// Counters we are interested in. Thread safe.
	/// </summary>
	public class EventsCountersDbValue
	{
		public SimpleCounter EntersCounter { get; }
		public SimpleCounter ExitsCounter { get; }
		public SimpleCounter CommentsCounter { get; }
		public SimpleCounter HighFiveCounter { get; }

		public EventsCountersDbValue()
		{
			EntersCounter = new SimpleCounter();
			ExitsCounter = new SimpleCounter();
			CommentsCounter = new SimpleCounter();
			HighFiveCounter = new SimpleCounter();
		}
	}
}