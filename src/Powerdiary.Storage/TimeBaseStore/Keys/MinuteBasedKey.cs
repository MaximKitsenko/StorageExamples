using System;

namespace Powerdiary.Storage.TimeBaseStore.Keys
{
	public class MinuteBasedKey : IDateTimeKeyConverter
	{
		public int Minute { get; }

		public MinuteBasedKey(int minute)
		{
			Minute = minute;
		}

		public DateTime GetKey(DateTime srcKey)
		{
			var mul = srcKey.Minute / Minute;
			var newMinKey = mul * Minute;
			return new DateTime(srcKey.Year, srcKey.Month, srcKey.Day, srcKey.Hour, newMinKey, 0, DateTimeKind.Utc);
		}

		public DateTime GetNextKey(DateTime srcKey)
		{
			return GetKey(srcKey.AddMinutes(Minute));
		}
	}
}