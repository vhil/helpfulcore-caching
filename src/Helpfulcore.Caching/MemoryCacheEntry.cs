using System;

namespace Helpfulcore.Caching
{
	public class MemoryCacheEntry
	{
		internal DateTime ExpiresAt { get; private set; }

		internal object Value { get; private set; }

		public MemoryCacheEntry(object value, TimeSpan expiresIn = default(TimeSpan))
		{
			if (expiresIn == default(TimeSpan))
			{
				expiresIn = TimeSpan.FromDays(365);
			}

			this.Value = value;
			this.ExpiresAt = DateTime.UtcNow.Add(expiresIn);
		}
	}
}
