using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Events
{
	/// <summary>
	/// enter-the-room
	/// </summary>
	public class UserEntered : Event
	{
		public ChatRoomId ChatRoomId { get; private set; }
		public SysInfo SysInfo { get; private set; }

		public UserEntered(SysInfo sysInfo, ChatRoomId chatRoomId)
		{
			ChatRoomId = chatRoomId;
			SysInfo = sysInfo;
		}
	}
}