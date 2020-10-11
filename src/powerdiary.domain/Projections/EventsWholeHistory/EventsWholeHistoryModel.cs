using System;
using System.Collections.Generic;

namespace Powerdiary.Domain.Projections.EventsWholeHistory
{
	/// <summary>
	/// Is not thread-safe.
	/// View is for viewing, not for modifying and storing something
	/// </summary>
	public class EventsWholeHistoryModel
	{
		public int EntersCounter { get; private set; }
		public int ExitsCounter { get; private set; }
		public int CommentsCounter { get; private set; }
		public int HighFiveCounter { get; private set; }
		public Dictionary<Guid, int> FivesToOther { get; private set; }

		public EventsWholeHistoryModel(int entersCounter, int exitsCounter, int commentsCounter, int highFiveCounter,
			Dictionary<Guid, int> fivesToOther)
		{
			EntersCounter = entersCounter;
			ExitsCounter = exitsCounter;
			CommentsCounter = commentsCounter;
			HighFiveCounter = highFiveCounter;
			FivesToOther = fivesToOther;
		}
	}
}