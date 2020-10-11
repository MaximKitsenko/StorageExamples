using System;

namespace Powerdiary.Storage.TimeBaseStore.Keys
{
	public class HourKey : IDateTimeKeyConverter
	{
		public DateTime GetKey(DateTime srcKey)
		{
			return new DateTime(srcKey.Year, srcKey.Month, srcKey.Day, srcKey.Hour, 0, 0, DateTimeKind.Utc);
		}

		public DateTime GetNextKey(DateTime srcKey)
		{
			return GetKey(srcKey.AddHours(1));
		}
	}
}