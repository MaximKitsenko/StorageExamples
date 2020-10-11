using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Powerdiary.Domain.Projections.EventsCountDetailedByOneMinute
{
	/// <summary>
	/// Is not thread-safe.
	/// View is for viewing, not for modifying and storing something
	/// </summary>
	public class EventsCountDetailedByOneMinuteViewModel
	{
		public int EntersCounter { get; private set; }
		public int ExitsCounter { get; private set; }
		public int CommentsCounter { get; private set; }
		public int HighFiveCounter { get; private set; }
		public Dictionary<Guid, Dictionary<Guid, int>> HighFiveDblIndex { get; }

		public EventsCountDetailedByOneMinuteViewModel(
			int entersCounter, 
			int exitsCounter, 
			int commentsCounter, 
			int highFiveCounter)
		{
			EntersCounter = entersCounter;
			ExitsCounter = exitsCounter;
			CommentsCounter = commentsCounter;
			HighFiveCounter = highFiveCounter;
			HighFiveDblIndex = new Dictionary<Guid, Dictionary<Guid, int>>();
		}

		public EventsCountDetailedByOneMinuteViewModel(
			int entersCounter, 
			int exitsCounter, 
			int commentsCounter, 
			int highFiveCounter, 
			Dictionary<Guid, Dictionary<Guid, int>> fivesByUserIndex)
		{
			EntersCounter = entersCounter;
			ExitsCounter = exitsCounter;
			CommentsCounter = commentsCounter;
			HighFiveCounter = highFiveCounter;
			HighFiveDblIndex = fivesByUserIndex;
		}
	}
}