namespace Powerdiary.Infrastructure
{
	public interface Handles<T>
	{
		void Handle(T message);
	}
}