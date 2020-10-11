using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Commands
{
	public class ExitChatRoom : Command
	{
		public readonly ChatRoomId ChatRoomId;
		public readonly int OriginalVersion;
		public readonly SysInfo SysInfo;

		public ExitChatRoom(ChatRoomId chatRoomId, int originalVersion, SysInfo sysInfo)
		{
			ChatRoomId = chatRoomId;
			OriginalVersion = originalVersion;
			SysInfo = sysInfo;
		}
	}
}