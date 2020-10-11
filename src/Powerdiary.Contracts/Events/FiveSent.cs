using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Events
{
	/// <summary>
	/// high-five-another-user
	/// </summary>
	public class FiveSent : Event
	{
		public ChatRoomId ChatRoomId { get; private set; }
		public UserId ToUserId { get; private set; }
		public SysInfo SysInfo { get; private set; }

		public FiveSent(SysInfo sysInfo, ChatRoomId chatRoomId, UserId toUserId)
		{
			ChatRoomId = chatRoomId;
			ToUserId = toUserId;
			SysInfo = sysInfo;
		}
	}
}