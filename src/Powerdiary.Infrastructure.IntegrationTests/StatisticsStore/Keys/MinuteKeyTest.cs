using System;
using System.Security.Cryptography.X509Certificates;
using FluentAssertions;
using NUnit.Framework;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.TimeBasedStore.IntegrationTests.StatisticsStore.Keys
{
	[TestFixture]
	public class MinuteKeyTest
	{
		[Test]
		public void GivenDateTime_WhenConvertDateTimeToMinuteKey_TheSameDateTimeReceivedButWithoutSeconds()
		{
			//arrange
			var dt = new DateTime(2020, 01, 02, 03, 04, 05);
			var converter = new MinuteKey();

			//act
			var result = converter.GetKey(dt);

			//assert
			result.Should().Be(new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0));
		}
		
	}
}