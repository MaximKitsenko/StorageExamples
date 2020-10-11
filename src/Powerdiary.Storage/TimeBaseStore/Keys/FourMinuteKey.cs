using System;

namespace Powerdiary.Storage.TimeBaseStore.Keys
{
	public class FourMinuteKey : IDateTimeKeyConverter
	{
		public DateTime GetKey(DateTime srcKey)
		{
			var mul = srcKey.Minute >> 2;
			var newMinKey = mul << 2;
			return new DateTime(srcKey.Year, srcKey.Month, srcKey.Day, srcKey.Hour, newMinKey, 0, DateTimeKind.Utc);
		}

		public DateTime GetNextKey(DateTime srcKey)
		{
			const int step = 4;
			return GetKey(srcKey.AddMinutes(step));
		}
	}
}