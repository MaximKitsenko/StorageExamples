using System;
using Powerdiary.Storage.TimeBaseStore.Engine;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Domain.Projections.EventsCountExtendedByFourMinute
{
	public static class InMemoryFourMinuteStore
	{
		public static SimpleTimeSeriesIndex<FourMinuteKey, EventsCountersDbValue> Index =
			new SimpleTimeSeriesIndex<FourMinuteKey, EventsCountersDbValue>();
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
		public SimpleIndex<Guid, SimpleCounter> HighFiveCounterIndex { get; }

		public EventsCountersDbValue()
		{
			EntersCounter = new SimpleCounter();
			ExitsCounter = new SimpleCounter();
			CommentsCounter = new SimpleCounter();
			HighFiveCounter = new SimpleCounter();
			HighFiveCounterIndex = new SimpleIndex<Guid,SimpleCounter>();
		}
	}
}