using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Commands
{
	public class SendComment : Command
	{
		public readonly ChatRoomId ChatRoomId;
		public readonly CommentText Text;
		public readonly int OriginalVersion;
		public readonly SysInfo SysInfo;

		public SendComment(ChatRoomId chatRoomId, CommentText text, int originalVersion, SysInfo sysInfo)
		{
			ChatRoomId = chatRoomId;
			Text = text;
			OriginalVersion = originalVersion;
			SysInfo = sysInfo;
		}
	}
}