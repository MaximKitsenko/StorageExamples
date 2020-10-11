using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Commands
{
	public class RenameChatRoom : Command
	{
		public readonly ChatRoomId ChatRoomId;
		public readonly string NewName;
		public readonly int OriginalVersion;
		public readonly SysInfo SysInfo;

		public RenameChatRoom(ChatRoomId chatRoomId, string newName, int originalVersion, SysInfo sysInfo)
		{
			ChatRoomId = chatRoomId;
			NewName = newName;
			OriginalVersion = originalVersion;
			SysInfo = sysInfo;
		}
	}
}