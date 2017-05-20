namespace Helpfulcore.Caching
{
    using System.Linq;
    using System.Collections.Concurrent;

    public class MemoryCacheRepository : ICacheRepository
    {
        public MemoryCacheRepository()
        {
            this.Entries = new ConcurrentDictionary<string, MemoryCacheEntry>();
        }

        public ConcurrentDictionary<string, MemoryCacheEntry> Entries { get; protected set; }

        public void Clear()
        {
            this.Entries.RemoveCacheEntries(this.Entries.Keys.ToArray());
            this.Entries.Clear();
        }
    }
}
