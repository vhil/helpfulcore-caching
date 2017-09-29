using System;
using System.Collections.Generic;

namespace Helpfulcore.Caching.Sitecore
{
    public class SitecoreCacheProvider : ISitecoreCacheProvider
    {
        protected ICacheRepository CacheRepository;
        protected ICacheProvider BaseProvider;

        public SitecoreCacheProvider(ICacheRepository cacheRepository)
        {
            this.CacheRepository = cacheRepository;
            this.BaseProvider = new CacheProvider(cacheRepository);
        }

        public virtual bool Add<T>(string key, T value, TimeSpan expiresIn = new TimeSpan(), bool clearOnPublish = false)
        {
            if (!clearOnPublish)
            {
                return this.BaseProvider.Add(key, value, expiresIn);
            }

            MemoryCacheEntry entry;
            if (this.CacheRepository.Entries.TryGetValue(key, out entry))
            {
                return false;
            }

            return this.CacheRepository.Entries.TryAdd(key, new SitecoreMemoryCacheEntry(value, expiresIn, true));
        }

        public virtual bool Set<T>(string key, T value, TimeSpan expiresIn = default(TimeSpan), bool clearOnPublish = false)
        {
            if (!clearOnPublish)
            {
                return this.BaseProvider.Set<T>(key, value, expiresIn);
            }

            MemoryCacheEntry entry;
            var cacheEntry = new SitecoreMemoryCacheEntry(value, expiresIn, true);

            if (this.CacheRepository.Entries.TryGetValue(key, out entry))
            {
                this.CacheRepository.Entries[key] = cacheEntry;
                return true;
            }

            return this.CacheRepository.Entries.TryAdd(key, cacheEntry);
        }

        public void Dispose()
        {
            this.BaseProvider.Dispose();
        }

        public bool Add<T>(string key, T value, TimeSpan expiresIn = new TimeSpan())
        {
            return this.BaseProvider.Add(key, value, expiresIn);
        }

        public T Get<T>(string key)
        {
            return this.BaseProvider.Get<T>(key);
        }

        public IDictionary<string, T> GetAll<T>(IEnumerable<string> keys)
        {
            return this.BaseProvider.GetAll<T>(keys);
        }

        public bool Remove(string key)
        {
            return this.BaseProvider.Remove(key);
        }

        public void RemoveAll(IEnumerable<string> keys)
        {
            this.BaseProvider.RemoveAll(keys);
        }

        public bool Set<T>(string key, T value, TimeSpan expiresIn = new TimeSpan())
        {
            return this.BaseProvider.Set(key, value, expiresIn);
        }

        public void FlushAll()
        {
            this.BaseProvider.FlushAll();
        }
    }
}
