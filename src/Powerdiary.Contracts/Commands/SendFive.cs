using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Commands
{
	public class SendFive : Command
	{
		public readonly ChatRoomId ChatRoomId;
		public readonly UserId ToUserId;
		public readonly int OriginalVersion;
		public readonly SysInfo SysInfo;

		public SendFive(ChatRoomId chatRoomId, UserId toUserId, int originalVersion, SysInfo sysInfo)
		{
			ChatRoomId = chatRoomId;
			ToUserId = toUserId;
			OriginalVersion = originalVersion;
			SysInfo = sysInfo;
		}
	}
}