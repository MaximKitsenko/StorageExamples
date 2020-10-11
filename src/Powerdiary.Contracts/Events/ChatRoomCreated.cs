using System;
using Powerdiary.Contracts.ValueObjects;
using Powerdiary.Infrastructure;

namespace Powerdiary.Contracts.Events
{
	public class ChatRoomCreated : Event
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public SysInfo SysInfo { get; private set; }

		public ChatRoomCreated(Guid id, string name, SysInfo sysInfo)
		{
			Id = id;
			Name = name;
			SysInfo = sysInfo;
		}
	}
}