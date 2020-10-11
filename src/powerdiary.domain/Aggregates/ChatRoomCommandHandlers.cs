using Powerdiary.Contracts.Commands;
using Powerdiary.Infrastructure;

namespace Powerdiary.Domain.Aggregates
{
	public class ChatRoomCommandHandlers
	{
		private readonly IRepository<ChatRoom> _repository;

		public ChatRoomCommandHandlers(IRepository<ChatRoom> repository)
		{
			_repository = repository;
		}

		public void Handle(CreateChatRoom message)
		{
			var item = new ChatRoom(message.ChatId.Id, message.Name);
			_repository.Save(item, -1);
		}

		public void Handle(RenameChatRoom message)
		{
			var item = _repository.GetById(message.ChatRoomId.Id);
			item.ChangeName(message.NewName, message.SysInfo);
			_repository.Save(item, item.Version );
		}

		public void Handle(EnterChatRoom message)
		{
			var chatRoom = _repository.GetById(message.ChatRoomId.Id);
			chatRoom.EnterUser(message.SysInfo,message.ChatRoomId);
			_repository.Save(chatRoom, chatRoom.Version);
		}

		public void Handle(ExitChatRoom message)
		{
			var chatRoom = _repository.GetById(message.ChatRoomId.Id);
			chatRoom.ExitUser(message.SysInfo, message.ChatRoomId);
			_repository.Save(chatRoom, chatRoom.Version);
		}

		public void Handle(SendFive message)
		{
			var chatRoom = _repository.GetById(message.ChatRoomId.Id);
			chatRoom.HighFive(message.SysInfo, message.ToUserId, message.ChatRoomId);
			_repository.Save(chatRoom, chatRoom.Version);
		}

		public void Handle(SendComment message)
		{
			var chatRoom = _repository.GetById(message.ChatRoomId.Id);
			chatRoom.SendComment(message.Text, message.ChatRoomId, message.SysInfo);
			_repository.Save(chatRoom, chatRoom.Version);
		}
	}
}