using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Powerdiary.Storage.BucketBasedStore.Engine;

namespace Powerdiary.Storage.Tests.BucketBasedStore.Engine
{
	[TestFixture]
	public class ConcurrentBucketTest
	{
		[Test]
		public void MessagesAddedToBucket_WhenGetMessagesWithTime_OnlyMessagesWithinStartEndTimeReturned()
		{
			// arrange
			var time1 = new DateTime(2020,01,02,03,04,05);
			var time2 = new DateTime(2020,01,02,03,04,06);
			var time3 = new DateTime(2020,01,02,03,04,07);
			var time4 = new DateTime(2020,01,02,03,04,08);
			var bucketId = time2.ToDayBucketKey();
			var concurrentBucket = new ConcurrentBucket<int>(bucketId);
			var messages = new List<KeyValuePair<DateTime,int>>()
			{
				Extensions.CreateKeyValuePair(time2,20),
				Extensions.CreateKeyValuePair(time1,10),
				Extensions.CreateKeyValuePair(time3,30),
				Extensions.CreateKeyValuePair(time4,33),
			};
			foreach (var msg in messages)
			{
				concurrentBucket.Add(msg.Key, msg.Value);
			}

			//act
			var messagesWithTime = concurrentBucket.GetMessagesWithTime(time2, time3).ToList();

			//assert
			messagesWithTime.Should().OnlyContain(x=>x.TimeKey>= time2 && x.TimeKey <= time3);
		}

		[Test]
		public void ThereIsBucketWithSpecialBucketId_WhenAddMessage_MessagesWithKeyWhichDoNotCorrespondBucketIdAreNotAdded()
		{
			// arrange
			var time1 = new DateTime(2020, 01, 02, 03, 04, 05);
			var time2 = new DateTime(2020, 01, 02, 03, 04, 05);
			var time3 = new DateTime(2020, 01, 03, 03, 04, 05);
			var bucketId = time2.ToDayBucketKey();
			var concurrentBucket = new ConcurrentBucket<int>(bucketId);
			var messages = new List<KeyValuePair<DateTime, int>>()
			{
				Extensions.CreateKeyValuePair(time2,20),
				Extensions.CreateKeyValuePair(time1,10),
				Extensions.CreateKeyValuePair(time3,30),
			};
			foreach (var msg in messages)
			{
				concurrentBucket.Add(msg.Key, msg.Value);
			}

			//act
			var messagesWithTime = concurrentBucket.GetMessagesWithTime(time2, time3).ToList();

			//assert
			messagesWithTime.Should().NotContain(x => x.TimeKey == time3);
		}

		[Test]
		public void ThereIsBucket_WhenAddMessagesWithTheSameKey_MessagesWithTheSameKeyAdded()
		{
			// arrange
			var time1 = new DateTime(2020, 01, 02, 03, 04, 05);
			var time2 = new DateTime(2020, 01, 02, 03, 04, 05);
			var time3 = new DateTime(2020, 01, 02, 03, 04, 05);
			var bucketId = time2.ToDayBucketKey();
			var concurrentBucket = new ConcurrentBucket<int>(bucketId);
			var messages = new List<MessageWithTimeKey<int>>()
			{
				new MessageWithTimeKey<int>(20,time2),
				new MessageWithTimeKey<int>(10,time1),
				new MessageWithTimeKey<int>(30,time3),
			};
			foreach (var msg in messages)
			{
				concurrentBucket.Add(msg.TimeKey, msg.Message);
			}

			//act
			var messagesWithTime = concurrentBucket.GetMessagesWithTime(time1, time3).ToList();

			//assert
			messagesWithTime.Should().BeEquivalentTo(messages);
		}
	}
}