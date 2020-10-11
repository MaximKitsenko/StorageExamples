using System;
using System.Collections.Generic;
using Powerdiary.Domain.Projections;
using Powerdiary.Domain.Projections.EventsCountByOneMinute;
using Powerdiary.Domain.Projections.EventsCountDetailedByOneMinute;
using Powerdiary.Domain.Projections.EventsCountExtendedByFourMinute;
using Powerdiary.Domain.Projections.EventsWholeHistory;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Domain
{
	public interface IReadModelFacade
	{
		IEnumerable<ChatRoomListDto> GetChatRooms();

		List<KeyValuePair<DateTime, EventsCountExtendedByFourMinuteModel>> GetOneMinuteEventsStatistics(
			DateTime start,
			DateTime end);

		List<KeyValuePair<DateTime, EventsCountExtendedByFourMinuteModel>>
			GetEventsCountExtendedByFourMinuteAggregatedByCustomTime(DateTime start,
				DateTime end, IDateTimeKeyConverter dateTimeKeyConverter);

		List<KeyValuePair<DateTime, EventsWholeHistoryModel>>
			GetEventsWholeHistoryAggregatedByCustomTime(DateTime start,
				DateTime end, IDateTimeKeyConverter dateTimeKeyConverter);

		List<KeyValuePair<DateTime, EventsCountByOneMinuteModel>> 
			GetEventsCountByOneMinuteAggregatedByCustomTime(
			DateTime start,
			DateTime end,
			IDateTimeKeyConverter dateTimeKeyConverter);

		List<KeyValuePair<DateTime, EventsCountDetailedByOneMinuteViewModel>>
			GetEventsCountDetailedByOneMinuteAggregatedByCustomTime(
				DateTime start,
				DateTime end,
				IDateTimeKeyConverter dateTimeKeyConverter);
	}
}