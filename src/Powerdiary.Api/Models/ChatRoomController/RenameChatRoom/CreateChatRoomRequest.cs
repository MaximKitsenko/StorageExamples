using System;

namespace Powerdiary.Api.Models.ChatRoomController.RenameChatRoom
{
	public class RenameChatRoomRequest
	{
		public string NewName;
		public Guid ChatRoomId;
	}
}