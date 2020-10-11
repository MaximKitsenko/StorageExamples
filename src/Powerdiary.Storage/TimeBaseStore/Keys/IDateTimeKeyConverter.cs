using System;

namespace Powerdiary.Storage.TimeBaseStore.Keys
{
	/// <summary>
	/// Kinda hash function - receives data and converts it to a key.
	/// But even big changes in src data leads to small changes in result
	/// </summary>
	public interface IDateTimeKeyConverter
	{
		DateTime GetKey(DateTime srcKey);
		DateTime GetNextKey(DateTime srcKey);
	}
}