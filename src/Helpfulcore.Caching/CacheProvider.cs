namespace Helpfulcore.Caching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CacheProvider : ICacheProvider
	{
	    protected readonly ICacheRepository CacheRepository;
	    protected bool IsDisposed;

	    public CacheProvider(ICacheRepository cacheRepository)
	    {
	        this.CacheRepository = cacheRepository;
	    }

        public virtual bool Add<T>(string key, T value, TimeSpan expiresIn = default(TimeSpan))
		{
			MemoryCacheEntry entry;
			if (this.CacheRepository.Entries.TryGetValue(key, out entry))
			{
				return false;
			}

			return this.CacheRepository.Entries.TryAdd(key, new MemoryCacheEntry(value, expiresIn));
		}

		public virtual T Get<T>(string key)
		{
			var obj = this.Get(key);

			if (obj != null && obj is T)
			{
				return (T)obj;
			}

			return default(T);
		}

		public virtual IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
		{
			return keys.Select(key => new { Key = key, Value = this.Get<T>(key) }).ToDictionary(k => k.Key, v => v.Value);
		}

		public virtual bool Remove(string key)
		{
		    this.CacheRepository.Entries.RemoveAndDispose(key);
		    return true;
		}

		public virtual void RemoveAll(IEnumerable<string> keys)
		{
			foreach (var key in keys)
			{
				this.Remove(key);
			}
		}

		public virtual bool Set<T>(string key, T value, TimeSpan expiresIn = default(TimeSpan))
		{
			MemoryCacheEntry entry;
			var cacheEntry = new MemoryCacheEntry(value, expiresIn);

			if (this.CacheRepository.Entries.TryGetValue(key, out entry))
			{
				this.CacheRepository.Entries[key] = cacheEntry;
				return true;
			}

			return this.CacheRepository.Entries.TryAdd(key, cacheEntry);
		}

		public virtual void FlushAll()
		{
            this.CacheRepository.Entries.RemoveAndDispose(this.CacheRepository.Entries.Keys.ToArray());
		    this.CacheRepository.Clear();
		}

		public virtual void Dispose()
		{
			if (!this.IsDisposed)
			{
				this.FlushAll();

				this.IsDisposed = true;
			}
		}

		protected virtual object Get(string key)
		{
			MemoryCacheEntry memoryCacheEntry;
			if (!this.CacheRepository.Entries.TryGetValue(key, out memoryCacheEntry))
			{
				return null;
			}

			if (memoryCacheEntry.ExpiresAt < DateTime.UtcNow)
			{
				this.CacheRepository.Entries.TryRemove(key, out memoryCacheEntry);
				return null;
			}

			return memoryCacheEntry.Value;
		}
	}
}
