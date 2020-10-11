using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Commands
{
	public class CreateChatRoom : Command
	{
		public readonly ChatRoomId ChatId;
		public readonly string Name;
		public readonly SysInfo SysInfo;

		public CreateChatRoom(ChatRoomId chatId, string name, SysInfo sysInfo)
		{
			ChatId = chatId;
			Name = name;
			SysInfo = sysInfo;
		}
	}
}