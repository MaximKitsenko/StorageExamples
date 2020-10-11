using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Powerdiary.Domain.Projections;
using Powerdiary.Domain.Projections.EventsCountByOneMinute;
using Powerdiary.Domain.Projections.EventsCountExtendedByFourMinute;
using Powerdiary.Domain.Projections.EventsCountDetailedByOneMinute;
using Powerdiary.Domain.Projections.EventsWholeHistory;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Domain
{
	public class ReadModelFacade : IReadModelFacade
	{
		public IEnumerable<ChatRoomListDto> GetChatRooms()
		{
			return InMemoryFakeDatabase.Index.Values;
		}

		public List<KeyValuePair<DateTime, EventsWholeHistoryModel>>
			GetEventsWholeHistoryAggregatedByCustomTime(
				DateTime start,
				DateTime end,
				IDateTimeKeyConverter dateTimeKeyConverter)
		{
			return EventsWholeHistoryView.GetEventsCountAggregated(start, end, dateTimeKeyConverter);
		}

		/// <summary>
		/// GetEventsCountByOneMinuteAggregatedByCustomTime
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="dateTimeKeyConverter"></param>
		/// <returns></returns>
		public List<KeyValuePair<DateTime, EventsCountByOneMinuteModel>>
			GetEventsCountByOneMinuteAggregatedByCustomTime(
				DateTime start,
				DateTime end,
				IDateTimeKeyConverter dateTimeKeyConverter)
		{
			return EventsCountByOneMinuteView.GetEventsCountAggregated(start, end, dateTimeKeyConverter);
		}

		/// <summary>
		/// GetEventsCountDetailedByOneMinuteAggregatedByCustomTime
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="dateTimeKeyConverter"></param>
		/// <returns></returns>
		public List<KeyValuePair<DateTime, EventsCountDetailedByOneMinuteViewModel>>
			GetEventsCountDetailedByOneMinuteAggregatedByCustomTime(
				DateTime start,
				DateTime end,
				IDateTimeKeyConverter dateTimeKeyConverter)
		{
			return EventsCountDetailedByOneMinuteView.GetEventsCountAggregated(start, end, dateTimeKeyConverter);
		}

		/// <summary>
		/// GetOneMinuteEventsStatisticsAggregatedByTwoMinute
		/// </summary>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <param name="dateTimeKeyConverter"></param>
		/// <returns></returns>
		public List<KeyValuePair<DateTime, EventsCountExtendedByFourMinuteModel>>
			GetEventsCountExtendedByFourMinuteAggregatedByCustomTime(
				DateTime start,
				DateTime end, 
				IDateTimeKeyConverter dateTimeKeyConverter)
		{
			return EventsCountExtendedByFourMinuteView.GetEventsCountAggregated(start, end, dateTimeKeyConverter);
		}

		public List<KeyValuePair<DateTime, EventsCountExtendedByFourMinuteModel>> GetOneMinuteEventsStatistics(
			DateTime start,
			DateTime end)
		{
			throw new NotImplementedException();
			//return EventsCountDetailedByOneMinuteProjection.GetEventsStatistics(start, end);
		}
	}
}