using System;

namespace Powerdiary.Api.Models.ChatRoomController.GetEventsStatistics
{
	public class GetEventsStatisticsRequest
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public int Granularity { get; set; }
	}
}