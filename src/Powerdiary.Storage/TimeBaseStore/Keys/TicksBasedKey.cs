using System;

namespace Powerdiary.Storage.TimeBaseStore.Keys
{
	public class TicksBasedKey : IDateTimeKeyConverter
	{
		private long Ticks { get; }

		public TicksBasedKey(long ticks)
		{
			Ticks = ticks;
		}

		public DateTime GetKey(DateTime srcKey)
		{
			var mult = srcKey.Ticks / this.Ticks;
			var bucket = mult * this.Ticks;
			return new DateTime(bucket);
		}

		public DateTime GetNextKey(DateTime srcKey)
		{
			return GetKey(srcKey.AddMinutes(1));
		}
	}
}