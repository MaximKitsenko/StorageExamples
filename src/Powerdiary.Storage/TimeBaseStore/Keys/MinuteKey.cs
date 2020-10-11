using System;

namespace Powerdiary.Storage.TimeBaseStore.Keys
{
	public class MinuteKey : IDateTimeKeyConverter
	{
		public DateTime GetKey(DateTime srcKey)
		{
			return new DateTime(srcKey.Year, srcKey.Month, srcKey.Day, srcKey.Hour, srcKey.Minute, 0, DateTimeKind.Utc);
		}

		public DateTime GetNextKey(DateTime srcKey)
		{
			return GetKey(srcKey.AddMinutes(1));
		}
	}
}