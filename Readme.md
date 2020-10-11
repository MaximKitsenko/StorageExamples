# Overview

## How data is stored `Powerdiary.Storage`

Theere are 2 storages represented in `Powerdiary.Storage`. This is where do we store things. In general they share the same idea - store values in buckets.

### Time based store `Powerdiary.Storage.TimeBaseStore`. [Faster reads than writes]

Bucket-based store, where bucket represented by `key` which is `DateTime`. The size of the bucket can be adjusted by choosing appropriate key from `Powerdiary.Storage.TimeBaseStore.Keys`.

**The main idea**

- precalculate data before we need it, to avoid (or at least reduce) calculations when we need to read data;
- since multiple events with the same timestamp are possible - do not store the events, but store system state at some point of time.

### Bucket store `Powerdiary.Storage.BucketBaseStore`. [Faster writes than reads]

**The main idea**

- store events in chronological order, use this storage as source of truth for calculating `Powerdiary.Storage.TimeBaseStore`;
- use BinarySearch `[O(log n)]` for searching events.

### Append-only stoe `Powerdiary.Storage.AppendOnlyStore`. [Faster writes than reads]

...comming soon.

## Quick start

- start `Powerdiary.Api` project frmo solution
- seed with test data: `curl --location --request POST 'https://localhost:44334/api/Chatroom/SeedWithTestData' \
--data-raw ''`
- read data: `curl --location --request GET 'https://localhost:44334/api/Chatroom/GetEventsCountByOneMinute' \
--header 'Content-Type: application/json' \
--data-raw '{
    "start":"2020-10-10 01:01:00" ,
    "end":"2020-10-10 01:06:02",
    "granularity":"5"
}'`
- read data: `curl --location --request GET 'https://localhost:44334/api/Chatroom/GetEventsWholeHistory' \
--header 'Content-Type: application/json' \
--data-raw '{
    "start":"2020-10-10 01:01:00" ,
    "end":"2020-10-12 01:06:02",
    "granularity":"1"
}'`
- read data: `curl --location --request GET 'https://localhost:44334/api/Chatroom/GetEventsCountDetailedByOneMinute' \
--header 'Content-Type: application/json' \
--data-raw '{
    "start":"2020-10-10 01:01:00" ,
    "end":"2020-10-10 01:06:02",
    "granularity":"2"
}'`
- read data: `curl --location --request GET 'https://localhost:44334/api/Chatroom/GetEventsCountExtendedByFourMinute' \
--header 'Content-Type: application/json' \
--data-raw '{
    "start":"2020-10-10 01:01:00" ,
    "end":"2020-10-10 01:06:02",
    "granularity":"5"
}'`


## By the way

Found this materials, may be it will be useful:
https://blog.discord.com/how-discord-stores-billions-of-messages-7fa6ec7ee4c7

https://stackoverflow.com/questions/935621/whats-the-difference-between-sortedlist-and-sorteddictionary/935631#935631
