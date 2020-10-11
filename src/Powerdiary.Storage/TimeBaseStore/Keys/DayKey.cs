using System;

namespace Powerdiary.Storage.TimeBaseStore.Keys
{
	public class DayKey : IDateTimeKeyConverter
	{
		public DateTime GetKey(DateTime srcKey)
		{
			return new DateTime(srcKey.Year, srcKey.Month, srcKey.Day, 0, 0, 0, DateTimeKind.Utc);
		}

		public DateTime GetNextKey(DateTime srcKey)
		{
			return GetKey(srcKey.AddDays(1));
		}
	}
}