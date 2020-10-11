using System;

namespace Powerdiary.Contracts.ValueObjects
{
	public class CommentId : IEquatable<CommentId>
	{
		public Guid Id { get; private set; }

		public CommentId(Guid id)
		{
			Id = id;
		}

		public bool Equals(CommentId other)
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
			return Equals((CommentId)obj);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public static CommentId CreateNew()
		{
			return new CommentId(Guid.NewGuid());
		}
	}
}