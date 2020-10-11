using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Commands
{
	public class EnterChatRoom : Command
	{
		public readonly ChatRoomId ChatRoomId;
		public readonly int OriginalVersion;
		public readonly SysInfo SysInfo;

		public EnterChatRoom(ChatRoomId chatRoomId, int originalVersion, SysInfo sysInfo)
		{
			ChatRoomId = chatRoomId;
			OriginalVersion = originalVersion;
			SysInfo = sysInfo;
		}
	}
}