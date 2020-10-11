using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Powerdiary.Contracts.Events;
using Powerdiary.Infrastructure;

namespace Powerdiary.Domain.Projections
{
	public class ChatsListView : 
		Handles<ChatRoomCreated>, 
		Handles<ChatRoomRenamed>
	{
		public void Handle(ChatRoomCreated message)
		{
			InMemoryFakeDatabase.Index[message.Id] = new ChatRoomListDto(message.Id, message.Name);
		}

		public void Handle(ChatRoomRenamed message)
		{
			InMemoryFakeDatabase.Index.TryGetValue(message.Id, out var item);
			item.Name = message.NewName;
		}
	}

	public class ChatRoomListDto
	{
		public Guid Id;
		public string Name;

		public ChatRoomListDto(Guid id, string name)
		{
			Id = id;
			Name = name;
		}
	}

	public static class InMemoryFakeDatabase
	{
		public static ConcurrentDictionary<Guid, ChatRoomListDto> Index = new ConcurrentDictionary<Guid, ChatRoomListDto>();
	}

}