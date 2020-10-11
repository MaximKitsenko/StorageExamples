using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Events
{
	/// <summary>
	/// leave-the-room
	/// </summary>
	public class UserExited : Event
	{
		public ChatRoomId ChatRoomId { get; private set; }
		public SysInfo SysInfo { get; private set; }

		public UserExited(SysInfo sysInfo, ChatRoomId chatRoomId)
		{
			ChatRoomId = chatRoomId;
			SysInfo = sysInfo;
		}
	}
}