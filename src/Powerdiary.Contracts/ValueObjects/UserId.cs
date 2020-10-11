using System;

namespace Powerdiary.Contracts.ValueObjects
{

	public class UserId : IEquatable<UserId>
	{
		public Guid Id { get; private set; }

		public static UserId SystemUserId => systemUserId;

		protected static UserId systemUserId = new UserId(Guid.Empty);

		public UserId(Guid id)
		{
			Id = id;
		}

		public bool Equals(UserId other)
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
			return Equals((UserId)obj);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
	}
}