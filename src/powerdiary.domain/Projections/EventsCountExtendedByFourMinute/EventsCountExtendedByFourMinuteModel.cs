using System;
using System.Collections.Generic;

namespace Powerdiary.Domain.Projections.EventsCountExtendedByFourMinute
{
	/// <summary>
	/// Counters we are interested in. Thread safe.
	/// </summary>
	public class EventsCountExtendedByFourMinuteModel
	{
		public int EntersCounter { get; private set; }
		public int ExitsCounter { get; private set; }
		public int CommentsCounter { get; private set; }
		public int HighFiveCounter { get; private set; }
		public Dictionary<Guid, int> UserSentFindIndex { get; }
		
		public EventsCountExtendedByFourMinuteModel(
			int entersCounter,
			int exitsCounter, 
			int commentsCounter,
			int highFiveCounter, 
			Dictionary<Guid, int> fivesByUserIndex)
		{
			EntersCounter = entersCounter;
			ExitsCounter = exitsCounter;
			CommentsCounter = commentsCounter;
			HighFiveCounter = highFiveCounter;
			UserSentFindIndex = fivesByUserIndex;
		}
	}
}