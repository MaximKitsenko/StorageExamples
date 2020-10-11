namespace Powerdiary.Domain.Projections.EventsCountByOneMinute
{
	/// <summary>
	/// Is not thread-safe.
	/// View is for viewing, not for modifying and storing something
	/// </summary>
	public class EventsCountByOneMinuteModel
	{
		public int EntersCounter { get; private set; }
		public int ExitsCounter { get; private set; }
		public int CommentsCounter { get; private set; }
		public int HighFiveCounter { get; private set; }

		public EventsCountByOneMinuteModel(int entersCounter, int exitsCounter, int commentsCounter, int highFiveCounter)
		{
			EntersCounter = entersCounter;
			ExitsCounter = exitsCounter;
			CommentsCounter = commentsCounter;
			HighFiveCounter = highFiveCounter;
		}

		public EventsCountByOneMinuteModel Add(int entersCounter, int exitsCounter, int commentsCounter, int highFiveCounter)
		{
			EntersCounter += entersCounter;
			ExitsCounter += exitsCounter;
			CommentsCounter += commentsCounter;
			HighFiveCounter += highFiveCounter;
			return this;
		}
	}
}