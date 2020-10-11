using System;

namespace Powerdiary.Contracts.ValueObjects
{

	public class ChatRoomId : IEquatable<ChatRoomId>
	{
		public Guid Id { get; private set; }

		public static ChatRoomId SystemChatRoomId => systemChatRoomId;

		protected static ChatRoomId systemChatRoomId = new ChatRoomId(Guid.Empty);

		public ChatRoomId(Guid id)
		{
			Id = id;
		}

		public bool Equals(ChatRoomId other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Id.Equals(other.Id);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((ChatRoomId)obj);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}