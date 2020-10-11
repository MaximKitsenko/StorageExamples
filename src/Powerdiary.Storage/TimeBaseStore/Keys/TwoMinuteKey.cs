using System;

namespace Powerdiary.Storage.TimeBaseStore.Keys
{
	public class TwoMinuteKey : IDateTimeKeyConverter
	{
		public DateTime GetKey(DateTime srcKey)
		{
			var newMinKey = srcKey.Minute >> 1;
			newMinKey <<= 1;
			return new DateTime(srcKey.Year, srcKey.Month, srcKey.Day, srcKey.Hour, newMinKey, 0, DateTimeKind.Utc);
		}

		public DateTime GetNextKey(DateTime srcKey)
		{
			const int step = 2;
			return GetKey(srcKey.AddMinutes(step));
		}
	}
}