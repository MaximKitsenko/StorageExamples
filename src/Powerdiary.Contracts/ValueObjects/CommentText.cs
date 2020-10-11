using System;

namespace Powerdiary.Contracts.ValueObjects
{
	public class CommentText : IEquatable<CommentText>
	{
		public string Text { get; private set; }

		public CommentText(string text)
		{
			Text = text;
		}

		public bool Equals(CommentText other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;
			return Text.Equals(other.Text);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != this.GetType()) return false;
			return Equals((CommentText)obj);
		}

		public override int GetHashCode()
		{
			return Text.GetHashCode();
		}
	}
}