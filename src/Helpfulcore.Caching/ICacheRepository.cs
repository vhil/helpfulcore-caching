namespace Helpfulcore.Caching
{
    using System.Collections.Concurrent;

    public interface ICacheRepository
    {
        ConcurrentDictionary<string, MemoryCacheEntry> Entries { get; }
        void Clear();
    }
}
