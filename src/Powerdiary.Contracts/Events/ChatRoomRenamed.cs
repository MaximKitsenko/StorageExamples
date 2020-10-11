using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Events
{
	public class ChatRoomRenamed : Event
	{
		public Guid Id { get; private set; }
		public string NewName { get; private set; }
		public SysInfo SysInfo { get; private set; }

		public ChatRoomRenamed(Guid id, string newName, SysInfo sysInfo)
		{
			Id = id;
			NewName = newName;
			SysInfo = sysInfo;
		}
	}
}