using System;
using Powerdiary.Contracts.Events;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Domain.Aggregates
{
	public class ChatRoom : AggregateRoot
	{
		private string _name;
		private Guid _id;
		public static bool DebugMode = false;

		public void Apply(ChatRoomCreated e)
		{
			_id = e.Id;
			_name = e.Name;
		}

		public void Apply(ChatRoomRenamed e)
		{
			_name = e.NewName;
		}

		public void Apply(UserEntered e)
		{
		}

		public void Apply(UserExited e)
		{
		}

		public void Apply(FiveSent e)
		{
		}

		public void Apply(CommentSent e)
		{
		}

		public void ChangeName(string newName, SysInfo sysInfo)
		{
			if (string.IsNullOrEmpty(newName)) throw new ArgumentException("newName");
			this.ApplyChange(new ChatRoomRenamed(_id, newName, DebugMode ? sysInfo : SysInfo.CreateSysInfo(sysInfo.UserId)));
		}

		public void EnterUser(SysInfo sysInfo, ChatRoomId chatRoomId)
		{
			this.ApplyChange(new UserEntered(DebugMode ? sysInfo : SysInfo.CreateSysInfo(sysInfo.UserId), chatRoomId));
		}

		public void ExitUser(SysInfo sysInfo, ChatRoomId chatRoomId)
		{
			this.ApplyChange(new UserExited(DebugMode ? sysInfo : SysInfo.CreateSysInfo(sysInfo.UserId), chatRoomId));
		}

		public void HighFive(SysInfo sysInfo, UserId toUserId, ChatRoomId chatRoomId)
		{
			this.ApplyChange(new FiveSent(DebugMode ? sysInfo : SysInfo.CreateSysInfo(sysInfo.UserId), chatRoomId,
				toUserId));
		}

		public void SendComment(CommentText text, ChatRoomId chatRoomId, SysInfo sysInfo)
		{
			this.ApplyChange(new CommentSent(DebugMode ? sysInfo : SysInfo.CreateSysInfo(sysInfo.UserId), chatRoomId,
				text, new CommentId(Guid.NewGuid())));
		}

		public override Guid Id
		{
			get { return _id; }
		}

		public ChatRoom()
		{
			// used to create in repository ... many ways to avoid this, eg making private constructor
		}

		public ChatRoom(Guid id, string name)
		{
			ApplyChange(new ChatRoomCreated(id, name, SysInfo.CreateSysInfo()));
		}
	}
}