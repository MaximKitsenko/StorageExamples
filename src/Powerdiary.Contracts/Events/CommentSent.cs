using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Events
{
	/// <summary>
	/// comment
	/// </summary>
	public class CommentSent : Event
	{
		public ChatRoomId ChatRoomId { get; private set; }
		public CommentId CommentId { get; private set; }
		public CommentText Text { get; private set; }
		public SysInfo SysInfo { get; private set; }

		public CommentSent(SysInfo sysInfo, ChatRoomId chatRoomId, CommentText text, CommentId commentId)
		{
			ChatRoomId = chatRoomId;
			Text = text;
			SysInfo = sysInfo;
			CommentId = commentId;
		}
	}
}