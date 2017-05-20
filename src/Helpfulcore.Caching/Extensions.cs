namespace Helpfulcore.Caching
{
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    public static class Extensions
    {
        public static void RemoveCacheEntries(this ConcurrentDictionary<string, MemoryCacheEntry> cacheEntries, string key)
        {
            MemoryCacheEntry entry;
            cacheEntries.TryRemove(key, out entry);
        }

        public static void RemoveCacheEntries(this ConcurrentDictionary<string, MemoryCacheEntry> cacheEntries, IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                cacheEntries.RemoveCacheEntries(key);
            }
        }
    }
}
