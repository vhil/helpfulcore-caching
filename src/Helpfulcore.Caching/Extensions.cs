namespace Helpfulcore.Caching
{
    using System.Collections.Generic;
    using System.Collections.Concurrent;

    public static class Extensions
    {
        public static void RemoveAndDispose(this ConcurrentDictionary<string, MemoryCacheEntry> cacheEntries, string key)
        {
            MemoryCacheEntry entry;
            if (cacheEntries.TryRemove(key, out entry))
            {
                entry?.Dispose();
                entry = null;
            }
        }

        public static void RemoveAndDispose(this ConcurrentDictionary<string, MemoryCacheEntry> cacheEntries, IEnumerable<string> keys)
        {
            foreach (var key in keys)
            {
                cacheEntries.RemoveAndDispose(key);
            }
        }
    }
}
