using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Newtonsoft.Json;
using Powerdiary.Api.Models.ChatRoomController.CreateChatRoom;
using Powerdiary.Api.Models.ChatRoomController.EnterChatRoom;
using Powerdiary.Api.Models.ChatRoomController.GetEventsStatistics;
using Powerdiary.Api.Models.ChatRoomController.RenameChatRoom;
using Powerdiary.Contracts.Commands;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Domain;
using Powerdiary.Domain.Aggregates;
using Powerdiary.Domain.Projections;
using Powerdiary.Domain.Projections.EventsCountByOneMinute;
using Powerdiary.Domain.Projections.EventsCountDetailedByOneMinute;
using Powerdiary.Domain.Projections.EventsCountExtendedByFourMinute;
using Powerdiary.Domain.Projections.EventsWholeHistory;
using Powerdiary.Infrastructure;
using Powerdiary.Storage.TimeBaseStore.Keys;

namespace Powerdiary.Api.Controllers
{
	public class ChatRoomController : ApiController
	{
		private FakeBus _bus;
		private IReadModelFacade _readmodel;

		public ChatRoomController()
		{
			_bus = ServiceLocator.Bus;
			_readmodel = new ReadModelFacade();
		}
		
		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("api/ChatRoom/GetEventsCountByOneMinute")]
		public JsonResult<List<KeyValuePair<DateTime, EventsCountByOneMinuteModel>>> GetEventsCountByOneMinute(
			[FromBody] GetEventsStatisticsRequest request)
		{
			var key = new TicksBasedKey(new TimeSpan(0, 0, request.Granularity, 0).Ticks);
			var res = _readmodel.GetEventsCountByOneMinuteAggregatedByCustomTime(request.Start, request.End, key);

			return Json(res);
		}

		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("api/ChatRoom/GetEventsCountDetailedByOneMinute")]
		public JsonResult<List<KeyValuePair<DateTime, EventsCountDetailedByOneMinuteViewModel>>> GetEventsCountDetailedByOneMinute(
			[FromBody] GetEventsStatisticsRequest request)
		{
			var key = new TicksBasedKey(new TimeSpan(0, 0, request.Granularity, 0).Ticks);
			var res = _readmodel.GetEventsCountDetailedByOneMinuteAggregatedByCustomTime(request.Start, request.End, key);

			return Json(res);
		}

		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("api/ChatRoom/GetEventsCountExtendedByFourMinute")]
		public JsonResult<List<KeyValuePair<DateTime, EventsCountExtendedByFourMinuteModel>>> GetEventsCountExtendedByFourMinute(
			[FromBody] GetEventsStatisticsRequest request)
		{
			var key = new TicksBasedKey(new TimeSpan(0, 0, request.Granularity, 0).Ticks);
			var res = _readmodel.GetEventsCountExtendedByFourMinuteAggregatedByCustomTime(request.Start, request.End, key);

			return Json(res);
		}

		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("api/ChatRoom/GetEventsWholeHistory")]
		public JsonResult<List<KeyValuePair<DateTime, EventsWholeHistoryModel>>> GetEventsWholeHistory(
			[FromBody] GetEventsStatisticsRequest request)
		{
			var key = new TicksBasedKey(new TimeSpan(0, 0, request.Granularity, 0).Ticks);
			var res = _readmodel.GetEventsWholeHistoryAggregatedByCustomTime(request.Start, request.End, key);

			return Json(res);
		}

		// GET api/values

		[System.Web.Http.HttpGet]
		[System.Web.Http.Route("api/ChatRoom/GetAllChatRooms")]
		public JsonResult<List<ChatRoomListDto>> GetAllChatRooms()
		{
			var chatRoomListDtos = _readmodel.GetChatRooms().ToList();
			return Json(chatRoomListDtos);
		}

		[System.Web.Http.HttpPost]
		[System.Web.Http.Route("api/ChatRoom/SeedWithTestData")]
		public OkResult SeedWithTestData()
		{
			InitializeWithTestData();

			return Ok();
		}

		[System.Web.Http.HttpPost]
		[System.Web.Http.Route("api/ChatRoom/CreateChatRoom")]
		public OkResult CreateChatRoom([FromBody]CreateChatRoomRequest request)
		{
			var chatRoomId = Guid.NewGuid();
			_bus.Send(new CreateChatRoom(new ChatRoomId(chatRoomId), request.Name, SysInfo.CreateSysInfo()));

			return Ok();
		}

		[System.Web.Http.HttpPost]
		[System.Web.Http.Route("api/ChatRoom/RenameChatRoom")]
		public OkResult RenameChatRoom([FromBody]RenameChatRoomRequest request)
		{
			_bus.Send(new RenameChatRoom(new ChatRoomId(request.ChatRoomId), request.NewName,-1, SysInfo.CreateSysInfo()));

			return Ok();
		}

		[System.Web.Http.HttpPost]
		[System.Web.Http.Route("api/ChatRoom/EnterChatRoom")]
		public OkResult EnterChatRoom([FromBody]EnterChatRoomRequest request)
		{
			_bus.Send(new EnterChatRoom(new ChatRoomId(request.ChatRoomId), -1, SysInfo.CreateSysInfo()));

			return Ok();
		}

		// PUT api/values/5

		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE api/values/5

		public void Delete(int id)
		{
		}

		private void InitializeWithTestData()
		{
			var chatRoomId = Guid.NewGuid();
			var user1 = new UserId(Guid.NewGuid());
			var user2 = new UserId(Guid.NewGuid());
			var user3 = new UserId(Guid.NewGuid());
			var user4 = new UserId(Guid.NewGuid());
			var user5 = new UserId(Guid.NewGuid());
			var millisecondsDelay = 3000;
			ChatRoom.DebugMode = true;

			_bus.Send(new CreateChatRoom(new ChatRoomId(chatRoomId), "newChatRoomName", SysInfo.CreateSysInfo()));
			Task.Delay(millisecondsDelay);

			var chatRooms = _readmodel.GetChatRooms();
			while (chatRooms.Count() < 1)
			{
				Task.Delay(500);
				chatRooms = _readmodel.GetChatRooms();
			}

			var room1Id = chatRooms.First().Id;

			var commands = new List<Message>
			{
				// 1st day
				new EnterChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user1, new DateTime(2020, 10, 10, 01, 02, 01,DateTimeKind.Utc))),
				new EnterChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user2, new DateTime(2020, 10, 10, 01, 02, 11,DateTimeKind.Utc))),
				new EnterChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user3, new DateTime(2020, 10, 10, 01, 02, 21,DateTimeKind.Utc))),
				new EnterChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user4, new DateTime(2020, 10, 10, 01, 02, 31,DateTimeKind.Utc))),
				new EnterChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user5, new DateTime(2020, 10, 10, 01, 02, 41,DateTimeKind.Utc))),

				new SendComment(new ChatRoomId(room1Id), new CommentText("from 1st user"), -1,
					SysInfo.CreateSysInfo(user1, new DateTime(2020, 10, 10, 01, 02, 51,DateTimeKind.Utc))),
				new SendComment(new ChatRoomId(room1Id), new CommentText("from 1st user"), -1,
					SysInfo.CreateSysInfo(user2, new DateTime(2020, 10, 10, 01, 03, 01,DateTimeKind.Utc))),
				new SendComment(new ChatRoomId(room1Id), new CommentText("from 1st user"), -1,
					SysInfo.CreateSysInfo(user3, new DateTime(2020, 10, 10, 01, 03, 11,DateTimeKind.Utc))),
				new SendComment(new ChatRoomId(room1Id), new CommentText("from 1st user"), -1,
					SysInfo.CreateSysInfo(user4, new DateTime(2020, 10, 10, 01, 03, 21,DateTimeKind.Utc))),
				new SendComment(new ChatRoomId(room1Id), new CommentText("from 1st user"), -1,
					SysInfo.CreateSysInfo(user5, new DateTime(2020, 10, 10, 01, 03, 31,DateTimeKind.Utc))),

				new SendFive(new ChatRoomId(room1Id), user2, -1,
					SysInfo.CreateSysInfo(user1, new DateTime(2020, 10, 10, 01, 03, 41,DateTimeKind.Utc))),
				new SendFive(new ChatRoomId(room1Id), user3, -1,
					SysInfo.CreateSysInfo(user1, new DateTime(2020, 10, 10, 01, 03, 51,DateTimeKind.Utc))),
				new SendFive(new ChatRoomId(room1Id), user4, -1,
					SysInfo.CreateSysInfo(user1, new DateTime(2020, 10, 10, 01, 04, 01,DateTimeKind.Utc))),
				new SendFive(new ChatRoomId(room1Id), user5, -1,
					SysInfo.CreateSysInfo(user1, new DateTime(2020, 10, 10, 01, 04, 11,DateTimeKind.Utc))),

				new SendFive(new ChatRoomId(room1Id), user3, -1,
					SysInfo.CreateSysInfo(user2, new DateTime(2020, 10, 10, 01, 04, 21,DateTimeKind.Utc))),
				new SendFive(new ChatRoomId(room1Id), user4, -1,
					SysInfo.CreateSysInfo(user2, new DateTime(2020, 10, 10, 01, 04, 31,DateTimeKind.Utc))),
				new SendFive(new ChatRoomId(room1Id), user5, -1,
					SysInfo.CreateSysInfo(user2, new DateTime(2020, 10, 10, 01, 04, 41,DateTimeKind.Utc))),

				new SendFive(new ChatRoomId(room1Id), user4, -1,
					SysInfo.CreateSysInfo(user3, new DateTime(2020, 10, 10, 01, 04, 51,DateTimeKind.Utc))),
				new SendFive(new ChatRoomId(room1Id), user5, -1,
					SysInfo.CreateSysInfo(user3, new DateTime(2020, 10, 10, 01, 05, 01,DateTimeKind.Utc))),

				new SendFive(new ChatRoomId(room1Id), user5, -1,
					SysInfo.CreateSysInfo(user4, new DateTime(2020, 10, 10, 01, 05, 11,DateTimeKind.Utc))),

				new ExitChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user1, new DateTime(2020, 10, 10, 01, 05, 21,DateTimeKind.Utc))),
				new ExitChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user2, new DateTime(2020, 10, 10, 01, 05, 31,DateTimeKind.Utc))),
				new ExitChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user3, new DateTime(2020, 10, 10, 01, 05, 41,DateTimeKind.Utc))),
				new ExitChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user4, new DateTime(2020, 10, 10, 01, 05, 51,DateTimeKind.Utc))),
				new ExitChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user5, new DateTime(2020, 10, 10, 01, 06, 01,DateTimeKind.Utc))),

				// 2nd day
				new EnterChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user1, new DateTime(2020, 10, 11, 01, 02, 01,DateTimeKind.Utc))),

				new SendComment(new ChatRoomId(room1Id), new CommentText("from 1st user"), -1,
					SysInfo.CreateSysInfo(user1, new DateTime(2020, 10, 11, 01, 02, 51,DateTimeKind.Utc))),

				new SendFive(new ChatRoomId(room1Id), user5, -1,
					SysInfo.CreateSysInfo(user4, new DateTime(2020, 10, 11, 01, 05, 11,DateTimeKind.Utc))),

				new ExitChatRoom(new ChatRoomId(room1Id), -1,
					SysInfo.CreateSysInfo(user5, new DateTime(2020, 10, 11, 01, 06, 01,DateTimeKind.Utc))),
			};

			foreach (var command in commands)
			{
				_bus.Send((dynamic)command);
			}

			_bus.Send(new CreateChatRoom(new ChatRoomId(new Guid()), "newChatRoom2", SysInfo.CreateSysInfo()));

			chatRooms = _readmodel.GetChatRooms();
			while (chatRooms.Count()<2)
			{
				Task.Delay(500);
				chatRooms = _readmodel.GetChatRooms();
			}
			ChatRoom.DebugMode = false;
		}
	}
}
